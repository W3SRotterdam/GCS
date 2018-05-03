$(document).ready(function () {
    /// *********
    /// VARIABLES
    /// *********
    var gcs = {
        elements: {
            query: $(".gcs_searchquery"),
            timing: $(".gcs_timing"),
            input: $(".gcs_input"),
            btn: $(".gcs_btn"),
            no_results: $('.gcs_no-results'),
            results: $(".gcs_results"),
            preloader: $(".gcs__preloadericon"),
            preloader_img: $(".gcs__preloadericon img"),
            results_count: $(".gcs_results_count"),
            loadmore: $(".gcs_loadmore"),
            pagination: $(".gcs_pagination"),
            infinitescroll: $(".gcs_infinite_scroll"),
            filter_filetype_select: $(".gcs_filetype_filter_select"),
            spelling: $(".gcs_spelling"),
            didyoumean: $(".gcs_didyoumean"),
            error: $(".gcs_error")
        },
        startIndex: 1,
        redirectpath: null,
        itemsPerPage: null,
        query: null,
        fileTypeFilter: "",
        docTypeFilter: "",
        resources: {
            getSettings: function () {
                return $.post("/umbraco/surface/GCSSettings/Get", { model: { CurrentURL: window.location.host + window.location.pathname } });
            },
            performSearch: function (term, startIndex) {
                return $.post("/umbraco/surface/GCSearch/Get", { model: { Query: term, StartIndex: startIndex, Filetype: gcs.fileTypeFilter, Section: gcs.docTypeFilter, CurrentNodeID: gcs.settings.currentNodeId } });
            },
            saveQuery: function (obj) {
                return $.post("/umbraco/surface/GCSQueries/UpdateClick", { model: obj });
            }
        },
        settings: {}
    };

    /// *********
    /// INIT CODE
    /// *********
    hideOptionalElements();

    gcs.resources.getSettings().then(function (setup) {
        var startparam = getQueryParam("startIndex");
        gcs.settings = setup;

        gcs.redirectpath = gcs.settings.redirectPath;
        gcs.itemsPerPage = gcs.settings.itemsPerPage;
        gcs.query = getQueryParam("gcs_q");
        gcs.fileTypeFilter = getQueryParam("gcs_ftype");
        gcs.docTypeFilter = getQueryParam("gcs_sect");
        gcs.startIndex = startparam != undefined && startparam != "" ? startparam : gcs.startIndex;

        addInputListeners();

        if ($($(gcs.elements.results)[0]).is(":empty")) {
            showLoader();
            gcs.resources.performSearch(gcs.query, gcs.startIndex).then(function (data) {
                setUpResult(data);
                hideLoader();
            });
        }
    });

    /// *********
    /// FUNCTIONS
    /// *********
    function setUpResult(data) {
        if (data.success) {
            if (data.totalCount != 0) {
                appendData(data);
                setupLoaders(data);
                setupFilters(data);
                addResultsListeners(data);
            } else if (gcs.settings.showSpelling && data.spelling != null && data.spelling != "") {
                showCorrectedQuery(data);
            } else {
                $(gcs.elements.no_results).show();
            }
        } else {
            $(gcs.elements.error).html("An error has occured. Please check your settings.");
        }
    }

    function addInputListeners() {
        $(gcs.elements.input).on("keypress", function (e) {
            var code = e.keyCode || e.which;
            if (code == 13) {
                search($(this).val());
            }
        });

        $(gcs.elements.btn).on("click", function () {
            var input = $(this).siblings(".gcs_input");
            search(input.val());
        });
    }

    function search(q) {
        document.location.href = gcs.redirectpath + "?gcs_q=" + q;
    }

    function addResultsListeners(data) {
        $(".gcs__result__link").on("click", function () {
            gcs.resources.saveQuery({
                Id: data.queryId,
                ClickURL: $(this).attr("href"),
                ClickTitle: $(this).attr("data-title")
            });
        });
    };

    function hideOptionalElements() {
        $(gcs.elements.loadmore).hide();
        $(gcs.elements.pagination).hide();
        $(gcs.elements.infinitescroll).hide();
        $(gcs.elements.timing).hide();
        $(gcs.elements.results_count).hide();
        $(gcs.elements.query).hide();
        $(gcs.elements.no_results).hide();
        $(gcs.elements.spelling).hide();
        $(gcs.elements.filter_filetype).hide();
        $(gcs.elements.filter_documenttype).hide();
    }

    function setupLoaders(data) {
        if ($(gcs.elements.loadmore) != undefined && gcs.settings.loadMoreSetUp.toLowerCase() == "button")
            setUpLazyLoad();

        if ($(gcs.elements.pagination) != undefined && gcs.settings.loadMoreSetUp.toLowerCase() == "pagination")
            setUpPagination(data.pagination);

        if (gcs.settings.loadMoreSetUp.toLowerCase() == "infinite")
            setUpInfiniteScroll(data);
    }

    function setupFilters(data) {
        if (gcs.settings.showFilterFileType) {
            $(gcs.elements.filter_filetype_select).show();

            var select = $(gcs.elements.filter_filetype_select).find("select");
            var firstoption = $(gcs.elements.filter_filetype_select).attr("data-first-option");

            if (select.length <= 0) {
                $(gcs.elements.filter_filetype_select).append("<div class='gcs gcs_filetype_filter_select'>" + data.filetypefilter + "</div>");
            }

            if (gcs.fileTypeFilter != "") {
                $(".gcs_filetype_filter_select select").val(gcs.fileTypeFilter);
            }

            $($(".gcs_filetype_filter_select option")[0]).html(firstoption);

            $(gcs.elements.filter_filetype_select).change(function () {
                gcs.fileTypeFilter = $(".gcs_filetype_filter_select option:selected").val();
                showLoader();
                gcs.resources.performSearch(gcs.query, gcs.startIndex).then(function (data) {
                    $(gcs.elements.results).html("");
                    setUpResult(data);
                    hideLoader();
                });
            });
        }
    }

    function appendData(data) {
        $(gcs.elements.results).append(data.list);

        if (gcs.settings.showQuery) {
            $(gcs.elements.query).show();
            var elem = $(gcs.elements.query).find(".gcs_query_append");
            if (elem.length <= 0) {
                $(gcs.elements.query).append("<span class='gcs gcs_query_append'>" + gcs.query + "</span>");
            } else {
                $($(elem)[0]).html(gcs.query);
            }
        }

        if (gcs.settings.showTiming) {
            $(gcs.elements.timing).show();
            var elem = $(gcs.elements.timing).find(".gcs_timing_append");
            if (elem.length <= 0) {
                $(gcs.elements.timing).append("<span class='gcs gcs_timing_append'>" + data.timing + "s. </span>");
            } else {
                $($(elem)[0]).html(gcs.timing);
            }
        }

        if (gcs.settings.showTotalCount) {
            $(gcs.elements.results_count).show();
            var elem = $(gcs.elements.results_count).find(".gcs_totalcount_append");
            if (elem.length <= 0) {
                $(gcs.elements.results_count).append("<span class='gcs gcs_totalcount_append'>" + data.totalCount + "</span>");
            } else {
                $($(elem)[0]).html(gcs.totalCount);
            }
        }

        if (gcs.settings.keepquery) {
            $(gcs.elements.input).val(gcs.query);
        }


        if (data.spelling == "") {
            $(gcs.elements.spelling).hide();
        }

        if (gcs.settings.showThumbnail) {
            $.each($(".gcs__result__thumnail"), function (index, value) {
                if ($(value).css("background-image") == "none") {
                    $(value).css({ "background-image": "url('" + gcs.settings.thumbnailFallback + "')" });
                }
            });
        } else {
            $.each($(".gcs__result__thumnail"), function (index, value) {
                $(value).hide();
            });
        }

        if (gcs.settings.loadMoreSetUp.toLowerCase() == "button") {
            if ((gcs.startIndex + gcs.itemsPerPage) >= data.totalCount || (data.totalCount <= gcs.itemsPerPage)) {
                $(gcs.elements.loadmore).hide();
            } else {
                $(gcs.elements.loadmore).show();
            }
        }

        if (gcs.settings.loadMoreSetUp.toLowerCase() == "pagination") {
            if (data.totalCount < gcs.itemsPerPage) {
                $(gcs.elements.pagination).hide();
            } else {
                $(gcs.elements.pagination).show();
            }
        }
    }

    function showCorrectedQuery(data) {
        if (gcs.settings.showSpelling) {
            $(gcs.elements.spelling).show();
            var elem = $(gcs.elements.spelling).find(".gcs_spelling_append");
            if (elem.length <= 0) {
                $(gcs.elements.spelling).append("<span class='gcs gcs_spelling_append'>" + data.spelling + "</span>")
            } else {
                $($(elem)[0]).html(data.spelling);
            }
        }
    }

    function setUpLazyLoad() {
        $(gcs.elements.loadmore).show();
        $(gcs.elements.loadmore).on("click", function () {
            gcs.startIndex = gcs.startIndex + gcs.itemsPerPage;
            showLoader();
            gcs.resources.performSearch(gcs.query, gcs.startIndex).then(function (data) {
                appendData(data);
                hiderLoader();
            });
        });
    }

    function setUpPagination(paginationHtml) {
        $(gcs.elements.pagination).show();
        $(gcs.elements.pagination).html(paginationHtml)
    }

    function setUpInfiniteScroll(data) {
        $(gcs.elements.infinitescroll).show();
        var boundingClient = getBoundingClient();
        var busy = false;

        $(window).on("scroll", function (e) {
            boundingClient = getBoundingClient();

            if (!busy && $(window).scrollTop() > boundingClient.bottom) {
                gcs.startIndex = Number(gcs.startIndex) + gcs.itemsPerPage;

                if (busy != true) {
                    if (gcs.startIndex < data.totalCount) {
                        showLoader();
                        gcs.resources.performSearch(gcs.query, gcs.startIndex).then(function (data) {
                            appendData(data);
                            busy = false;
                            hiderLoader();
                        });
                    }
                }
                busy = true;
            }
        });
    }

    function showLoader() {
        if (gcs.settings.preloaderIcon && gcs.settings.preloaderIcon != "") {
            $(gcs.elements.preloader).css({ "display": "block" });
            $(gcs.elements.preloader_img).attr("src", gcs.settings.preloaderIcon);
        } else {
            $(gcs.elements.preloader).css({ "display": "none" });
        }
    }

    function hideLoader() {
        $(gcs.elements.preloader).css({ "display": "none" });
    }

    function getBoundingClient() {
        return $(gcs.elements.results)[0].getBoundingClientRect();
    }

    function getQueryParam(key) {
        key = key.replace(/[*+?^$.\[\]{}()|\\\/]/g, "\\$&");
        var match = location.search.match(new RegExp("[?&]" + key + "=([^&]+)(&|$)"));
        return match && decodeURIComponent(match[1].replace(/\+/g, " "));
    }
});