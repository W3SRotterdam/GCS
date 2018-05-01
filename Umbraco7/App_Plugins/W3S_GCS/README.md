<sub>Download: https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor to view this file in VS</sub>

<br />

# GCS - Google Custom Search

GCS is W3S developed module that allows a website to utilize the GCS JSON API and show search results
on any page. It works as simple as putting a few mark-up elements on a page and adding a minified javascript file to your
shared layout template or webpack/gulp/grunt workflow.

### Basic Installation
A basic installation only needs two html elements to be inserted in your template alongside some classes.
You'll also need to reference the .js file that come alongside this package. 

GCS also creates some tables in the Umbraco database because it stores it's settings there.
<br />

#### Scripts
The .js files are automatically copied to your project by utilizing the post-build actions. Add a reference to the following script file either in a _layout.cshtml file or
in a bundles file.
```
<script src="~/App_Plugins/W3S_GCS/Scripts/gcsearch.js" type="text/javascript"></script>
```
<br />

#### Search input
To allow a user to search throughout your website you will need to add a input field on any page.
Copy the following snippet anywhere in one of your templates to add the input field. 

```
<input type="text" class="gcs gcs_input" name="text" placeholder="">
```
<br />

#### Search results
Secondly an element where the search results will be appended to needs to be iserted on your results page.
Copy the following snippet on your search results page.


```
<div class="gcs gcs_results"></div>
```

Now navigate to the Umbraco backoffice `GCS` section to further configure your installation.
If this section is not available then browse to the users configuration manager and allow the `GCS` section to be visible. 

<br />

#### Basic back-office configuration
Some back-office configuration is needed to get the GCS up and running. 

First set-up a new search engine instance at https://cse.google.com/cse/all

Secondly in Umbraco navigate to `GCS` > `Settings` > `Basic set-up`.  Here you will find some properties that must be filled in order for the module to work. 

1. *Items per page* The number of search results displayed per page.
2. *Load more set-up* Configure the way in which you want your users to load more search results.

The last step is to navigate in Umbraco to `GCS` > `Settings` > `Advanced set-up`. Here you will find some properties that must be filled in order for the module to work. 


1. *BaseURL* The google search API base URL. Default: https://www.googleapis.com/customsearch/v1
2. *CXKey** Can be found in the Google CS console.
3. *APIKey* Can be found in the Google CS console.
4. *Redirect node* The node on which you've placed the search results markup.
<br />

All these steps should set-up a basic google custom search installation for you.
<br />


### Advanced Installation
The basic installation covers a most fundamental implementation of the GCS module. 
If you want to add more functionalities such as a lazy loader, pagination, search button etc. you can find the
installation instructions here.


#### Query text
Allow to show the search term for which the current results are shown.
Insert the following snippet.

```
<div class="gcs gcs_searchquery"></div>
```

#### Search timing
Shows how long the search query took by inserting the following snippet.

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
<input type="button" class="gcs gcs_pagination" value="" />
```

#### File type filter
Allows to search based on file type. Insert the following snippet.

```
<span class="gcs gcs_filter_filetype" data-first-option="--Search by file type--"></span>
```

#### Document type filter 
Allows to search based on document type. Insert the following snippet.

```
<span class="gcs gcs_filter_documenttype" data-first-option="--Search in area--"></span>
```

#### No results
Allows to show a message if no results are found. Insert the following snippet.

```
<div class="gcs gcs_no-results">No results found.</div>
```


#### Spelling
Allows to show a corrected query 

```
<span class="gcs gcs_spelling">No results were found with the given search term. Did you mean: </span>
```
<br />

### Styling
Below you can find an overview of all the classed that are use alongside a short description on what it does.


| Class | Descriptions |
| ------ | ------ |
| .gcs | main class |
| .gcs\_searchquery | placeholder where the search term will be appended to |
| .gcs\_timing |  |
| .gcs\_results\_count | total count of search results |
| .gcs\_filter_filetype | file type filter | 
| .gcs\_filter_documenttype | document type filter | 
| .gcs\_spelling | Corrected query if available | 
| .gcs\_no-results | message if no results were found |
| .gcs\_results | results placeholder element |
| .gcs\_input | input to enter search term |
| .gcs\_btn | button to execute search |
| .gcs\_results | placeholder to append results to |
| .gcs\_loadmore | button to load more results |
| .gcs\_pagination | paging unit |
| .gcs\_gcs_error | error messages |
| .gcs\_\_filters | |
| .gcs\_doctypefilter\_btn | |
| .gcs\_filetypefilter\_btn | |
| .gcs\_pagination\_\_container | |
| .gcs\_pagination\_\_title | |
| .gcs\_results_partial\_\_list | |
| .gcs\_\_result\_\_thumnail | |
| .gcs\_\_result\_\_link | |
| .gcs\_\_result\_\_title | |
| .gcs\_\_result\_\_text | |
| .gcs\_\_result\_\_text-link | |


### Manage the GCS Settings via the backoffice
With this package comes a custom section to allow editors to customize their GCS settings, and search experience. 
To make this page visible for a use please follow the following steps. Go to `Users` section > `Users` node. Now select the desired user and go to `Sections` property > click `GCS`. Then hit save and refresh the page.