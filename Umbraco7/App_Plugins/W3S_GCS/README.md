

# ![alt text](https://w3s.nl/Content/img/w3s-logo.png "W3S") GCS 

Google Custom Search plugin for Umbraco v7.x

<br/>

## About
This project was started since Google announced that they discontinued their Site Search service as of april 2018. As an alternative this plugin for Umbraco utilizes the Google Custom Search API to retrieve search results and display them on a page.

Developed by **<https://w3s.nl>**

<br/>

## Installation

### 1. Back office configuration
After you've installed this plugin some mandatory configuration needs to be performed.
Whilst in the Umbraco backoffice navigate to GCS > Settings and configure the following fields:

- `CX Key`            > The custom search engine ID to use for this request. Visit **<https://cse.google.com/all>** to create a new search engine and retrieve the token id.
- `API Key`           > JSON/Atom Custom Search API requires the use of an API key. Visit **<https://console.developers.google.com/apis/credentials>** to create an API key or to retrieve an exisiting one. Do not forget to enable the Google Custom Search API via Library.

Optional:
- `Development URL`  > If the application is running on a development domain enter the URL of the domain you which to search (as configured in the custom search engine). Note that this is only intended for testing purposes. When running a umbraco application with multiple websites refer to "2B Multiple domain configuration"

### 2 A Single domain configuration
If you're running an umbraco installation serving just one website then follow these steps:

- 1 Allow the `Search` document type to be created in the content tree. This may me done via the `Developer` section.
- 2 Add the search page beneath a node of your choice in the content tree.


### 2 B Multiple domain configuration
If you're running an umbraco installation which serves multiple websites follow these steps:

- 1 Check if the domain names are correctly configured at the top level (homepage) of the content tree. This may be done in the context menu via `Culture and Hostnames`.
- 2 Allow the `Search` document type to be created in the content tree. This may me done via the `Developer` section.
- 3 Add a search node below each root node of the domain to allow search results to be appended.


### 3. Test

Navigate to the search page you've just created and give it a try.

You can add a search input anywhere in one of your website templates file where it will automatically redirect to the corresponding search page after a query.
Copy the following snippet to one of your templates to add a new search input field. 

```
<input type="text" class="gcs gcs_input" name="text" placeholder="">
```

<br/>

## Optional set-up

### 1. Further template configuration
The above basic installation covers a most fundamental implementation of the GCS module. If you wish to add more functionalities such as lazy loading, pagination, corrected queries etc.
you can add additional elements to your search template file.

(The elements below are already added to the template that belongs to the `Search` document type.)

#### Query text
Allows to show the search term for which the current results are shown. Insert the following snippet.

```
<div class="gcs gcs_searchquery"></div>
```

#### Search timing
Show the search duration by inserting the following snippet.

```
<div class="gcs gcs_timing"></div>
```

#### Results count
Shows the total results count

```
<div class="gcs gcs_results_count"></div>
```

#### Load more button
If you want to allow your users to load more results via a button click then insert the following snippet.

```
<input type="button" class="gcs gcs_loadmore" value="" />
```

#### Load more pagination
If you want to allow your users to load more results via pagination then insert the following snippet.

```
<div class="gcs gcs_pagination"></div>
```

#### Load more infinite scroll
If you want to allow your users to load more results via infinite scroll then insert the following snippet.

```
<div class="gcs gcs_infinite_scroll"></div>
```

#### File type filter
Allows to search based on file type. Insert the following snippet.

```
<span class="gcs gcs_filetype_filter_select" data-first-option="--Search by file type--"></span>
```

#### No results
Show a message if no search results are found. Insert the following snippet.

```
<div class="gcs gcs_no-results">No results found.</div>
```

#### Spelling
Show a corrected query 

```
<span class="gcs gcs_spelling">No results were found with the given search term. Did you mean: </span>
```

#### Preloader icon
Show a preloader icon while the search results are retrieved.

```
<div class="gcs gcs__preloadericon">
    <img src="" />
</div>
```

<br/>

### 2. Styling 
To add an additional layer of styling navigate to `stylesheets` > `w3s-gcs`.  
Below you may can an overview of all classes used throughout the plugin:

| Class | Description |
| ------ | ------ |
| .gcs | main class |
|  |  |
| **two basic markup elements** | * |
| .gcs\_input | input to enter search term |
| .gcs\_results | gcs results container |
|  |  |
| **optional elements** | * |
| .gcs\_searchquery | placeholder where the search term will be appended to |
| .gcs\_timing | duration of the search |
| .gcs\_results_count | total count of search results |
| .gcs\_filetype_filter_select | select element of the filetype filter |
| .gcs\_spelling | corrected query if available | 
| .gcs\_\_preloadericon | preloader icon shown when results are loaded | 
| .gcs\_no-results | message if no results were found |
| .gcs\_loadmore | button to load more results |
| .gcs\_pagination | paging unit |
| .gcs\_infinite_scroll | infinite scroller |
| .gcs\_gcs_error | error messages |
|  |  |
| **rendered template > results** | * |
| .gcs\_\_results\_\_row | results placeholder element row |
| .gcs\_\_results\_\_row\_\_col | results placeholder element row col |
| .gcs\_\_results\_\_list | placeholder to append results to |
| .gcs\_\_result\_\_thumnail | thumbnail image either from gcs api or fallback image |
| .gcs\_\_result\_\_thumnail\_\_image | the actual thumbnail image  |
| .gcs\_\_result | single result container  |
| .gcs\_\_result\_\_link | link to a single instance of the search results  |
| .gcs\_\_result\_\_title | title of a single instance of the search results  |
| .gcs\_\_result\_\_text | html formatted body text of a single instance of the search results |
| .gcs\_\_result\_\_text\_\_plain | plain body text of a single instance of the search results |
| .gcs\_\_result\_\_url | url of a single instance of the search results |
|  |  |
| **rendered template > spelling** | * |
| .gcs\_\_correctedquery | placeholder for the corrected query |
|  |  |
| **rendered template > pagination** | * |
| .gcs\_\_pagination | container of the pagination |
| .gcs\_\_pagination\_\_title | page title  |
| .gcs\_\_pagination\_\_btn | pagination button |
| .gcs\_\_pagination\_\_first | go to the first page |
| .gcs\_\_pagination\_\_previous | go to the previous page  |
| .gcs\_\_pagination\_\_next | go to the next page |
| .gcs\_\_pagination\_\_last | go to the last page |
|  |  |
| **rendered template > filetype filter** | * |
| .gcs\_\_filetypefilter | container of the filetype filter |
| .gcs\_\_filetypefilter\_\_select | select element |