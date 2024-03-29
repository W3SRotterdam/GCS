# W3S GCS
Google Custom Search plugin for Umbraco v8.x. 
Running this solution requires you to run visual studio edition 2017 or higher.

Looking for older versions?  
Umbraco v7.1.x / v7.2.x See: https://github.com/W3S-uORM/GCS/tree/umbraco-v7.1-v7.2  
Umbraco v7.3.x / v7.5.x See: https://github.com/W3S-uORM/GCS/tree/umbraco-v7.3-v7.5  
Umbraco v7.6.x or higher. See https://github.com/W3S-uORM/GCS/tree/umbraco-v7.6-v7.14  

## About
This project was started since Google has announced that they will discontinue their Site Search service as of april 2018. As an alternative this plugin for Umbraco uses the Google Custom Search API to retrieve search results and display them on a page.

Developed by W3S > https://w3s.nl/ We are builders and creators of online platforms with multiple user groups, self-service portals and e-commerce systems.

## Installation
This plugin is made available as an `Umbraco package` to be directly installed via the backoffice.
It's also made available via `Nuget` to be added directly to your project. 

### Umbraco package

#### 1. Install the package
Login to the Umbraco backoffice and navigate to the Developers section. Search for `w3s google custom search` and install the package.
After installation the GCS section is allowed for the administrator (alias: admin) group. If you're user account belongs to another user group the allow the GCS to be displayed.

For more information about configuration navigate to `GCS` > `Readme`.

### Using Nuget 
The plugin is also available as Nuget package; https://www.nuget.org/packages/W3S-GCS/ 
Please follow the instructions below to set-up a fully functioning GCS plugin inside you Umbraco project.

Run ``` Install-Package W3S-GCS ```in Package Manager Console.

#### 0. Add the GCS section to a user group.
You will need to allow your current Umbraco User access to the GCS section via the Umbraco backoffice. You may find these settings at the Users section which is accessible via the left navigational panel.

#### 1. Create a search page
GCS needs a page where it will display the search results. Therefore, create a new document type with template in the umbraco backoffice. Remember the type alias since it will be needed in further configuration.

#### 2. Back office configuration
After you've installed this plugin some mandatory configuration fields inside the GCS section needs to be set.
In the Umbraco backoffice navigate to GCS and configure the following fields:

- `Base URL`          > https://www.googleapis.com/customsearch/v1
- `CX Key`            > The custom search engine ID to use for this request. Visit https://cse.google.com/all to create a new search engine and retrieve the token id.
- `API Key`           > JSON/Atom Custom Search API requires the use of an API key. Go to https://console.developers.google.com/apis/credentials to create an API key or to retrieve one. Do not forget to enable the Google Custom Search API via Library.
- `Redirect alias`    > Enter the document type alias of the search results page you've created earlier.
- `Development URL`   > When working on a environment other than your live environment enter the absolute (including scheme) indexed domain name.

Other configuration fields are optional. For each field a short description of it's functionality is given below the label.

#### 3. Add dependencies 
GCS required jQuery to be included in your project. Add a reference to jquery either in a _layout.cshtml file or use a bundler (like webpack)

```
<script src="https://code.jquery.com/jquery-3.3.1.min.js" type="text/javascript"></script>
```

Next to jQuery add a reference to the following script file either in a _layout.cshtml file or use a bundler (like webpack)
```
<script src="~/App_Plugins/W3S_GCS/Scripts/gcsearch.min.js" type="text/javascript"></script>
```

#### 4. Add domains to the cultures & hostnames 
The plugin requires the domain names to be configured at the top level (homepage) of the content tree. 

#### 5. Configure the templates
A basic installation of GCS only needs two html elements to be inserted in the template file you've created whilst creating the document type. 

##### Search input
To allow a user to search throughout your website you will need to add a input field on any desired page.
Copy the following snippet anywhere in one of your templates to add a search input field. 

```
<input type="text" class="gcs gcs_input" name="text" placeholder="">
```

##### Search results
Secondly, to append the search results, add the following element to your search results template.

```
<div class="gcs gcs_results"></div>
```

<br />

This basic installation covers a most fundamental implementation of the GCS module. 
If you want to add more functionalities such as a lazy loader, pagination, search button etc. you can find the
installation instructions below.

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

### 6. Styling 
To add an additional layer of styling please refer to this overview of all classes that are used throughout the plugin.

| Class | Description |
| ------ | ------ |
| .gcs | main class |
|  |  |
| **two basic markup elements** | * |
| .gcs_input | input to enter search term |
| .gcs_results | gcs results container |
|  |  |
| **optional elements** | * |
| .gcs_searchquery | placeholder where the search term will be appended to |
| .gcs_timing | duration of the search |
| .gcs_results_count | total count of search results |
| .gcs_filetype_filter_select | select element of the filetype filter |
| .gcs_spelling | corrected query if available | 
| .gcs__preloadericon | preloader icon shown when results are loaded | 
| .gcs_no-results | message if no results were found |
| .gcs_loadmore | button to load more results |
| .gcs_pagination | paging unit |
| .gcs_infinite_scroll | infinite scroller |
| .gcs_gcs_error | error messages |
|  |  |
| **rendered template > results** | * |
| .gcs__results__row | results placeholder element row |
| .gcs__results__row__col | results placeholder element row col |
| .gcs__results__list | placeholder to append results to |
| .gcs__result__thumnail | thumbnail image either from gcs api or fallback image |
| .gcs__result__thumnail__image | the actual thumbnail image  |
| .gcs__result | single result container  |
| .gcs__result__link | link to a single instance of the search results  |
| .gcs__result__title | title of a single instance of the search results  |
| .gcs__result__text | html formatted body text of a single instance of the search results |
| .gcs__result__text__plain | plain body text of a single instance of the search results |
| .gcs__result__url | url of a single instance of the search results |
|  |  |
| **rendered template > spelling** | * |
| .gcs__correctedquery | placeholder for the corrected query |
|  |  |
| **rendered template > pagination** | * |
| .gcs__pagination | container of the pagination |
| .gcs__pagination__title | page title  |
| .gcs__pagination__btn | pagination button |
| .gcs__pagination__first | go to the first page |
| .gcs__pagination__previous | go to the previous page  |
| .gcs__pagination__next | go to the next page |
| .gcs__pagination__last | go to the last page |
|  |  |
| **rendered template > filetype filter** | * |
| .gcs__filetypefilter | container of the filetype filter |
| .gcs__filetypefilter__select | select element |
