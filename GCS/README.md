Download: https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor to view this file in VS

# GCS - Google Custom Search

GCS is W3S developed module that allows a website to utilize the GCS JSON API and show search results
on any page. It works as simple as putting a few mark-up elements on a page and adding a .js file to your
shared layout template or webpack/gulp/grunt workflow.

### Basic Installation
A basic installation only needs two html elements to be inserted in you template alongside some classes.
You also need to reference the .js file that come alongside this package. GCS also needs it's own database so make
sure you've already set on up.

#### Database
You need to set-up a database to use the GCS module. 
If this is done then add the following connection string to the root web.config file of your project and replace 
`[servername]`, `[dbname]`, `[dbuser]` and `[dbuser_password]` with the details of the newly created database.

```
<!--<add name="GCSEntities" connectionString="server=[servername];database=[dbname];user id=[dbuser];password=[dbuser_password]" providerName="System.Data.SqlClient" />-->
```

#### Scripts
The .js files are automatically copied to your project by utilizing the post-build actions. Add a reference to the following script file:
```
<script src="/Scripts/gcs/gcsearch.js" type="text/javascript"></script>
```

#### Search input
To allow a user to search through your website you will need to add a input field on the page.
Copy the following snippet anywhere in one of your templates to add the input field. 

```
<input type="text" class="gcs gcs_input" name="text" placeholder="Zoeken">
```

#### Search results
Secondly a element where the search results will be appended to needs to be iserted on your results page/ 
Copy the following snippet.


```
<div class="gcs gcs_results"></div>
```

This should set-up a basic google custom search installation for you. Go to the Umbraco backoffice `GCS` section to further configure your installation.


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


#### Search button
If you want to allow your users to search via a button then click insert the following snippet.

```
<input type="button" class="gcs gcs_btn" value="zoek" />
```

#### Search timing
Shows how long the search query took by inserting the following snippet.

```
<div class="gcs gcs_timing"></div>
```


#### No results
Allows to show a message if no results are found.
Insert the following snippet.

```
<div class="gcs gcs_no-results">Er zijn geen zoekresultaten gevonden met de opgegeven zoekterm.</div>
```


#### Results count
Shows the total results count

```
<div class="gcs gcs_results"></div>
```


### Styling
To more easily get started on styling you could start by copying all the class inside `/Content/less/gcs.less` to your
own style sheet. 
Below you can find an overview of all the classed that are use alongside a short description on what it does.

| Class | Descriptions |
| ------ | ------ |
| .gcs | main class |
| .gcs_searchquery | placeholder where the search term will be appended to |
| .gcs_no-results | message if no results were found |
| .gcs_input | input to enter search term |
| .gcs_btn | button to execute search |
| .gcs_results | placeholder to append results to |
| .gcs_results_count | total count of search results |
| .gcs_loadmore | button to load more results |
| .gcs_pagination | paging unit |
| .gcs_infinite_scroll | infinite scroller |
| .gcs_timing |  |
| x | x |
| .gcs_results_partial |  |
| .gcs_results_partial__list |  |
| .gcs__result__link |  |
| .gcs__result__title |  |
| .gcs__result__text |  |
| .gcs__result__text-link |  |


### Manage the GCS Settings via the backoffice
With this package comes a custom section to allow editors to customize their GCS settings, and search experience. 
To make this page visible for a use please follow the following steps. Go to `Users` section > `Users` node. Now select the desired user and go to `Sections` property > click `GCS`. Then hit save and refresh the page.


#### Select redirect node

#### Number of search results

#### Lazy load vs. pagination



.gcs {}
.gcs_searchquery {}
.gcs_no-results {}
.gcs_input {}
.gcs_btn {}
.gcs_results {}
.gcs_results_count {}
.gcs_loadmore {}
.gcs_pagination {}
.gcs_infinite_scroll {}
.gcs_timing {}
.gcs_results_partial {}
.gcs_results_partial__list {}
.gcs__result__link {}
.gcs__result__title {}
.gcs__result__text {}
.gcs__result__text-link {}