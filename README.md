# W3S GCS
Google Custom Search plugin for Umbraco v7.x

## About
This project was started since Google has announced that they will discontinue their Site Search service as of april 2018. As an alternative this plugin for Umbraco uses the Google Custom Search API to retrieve search results and display them on a page.

## Installation
#### Nuget 
The plugin is available as Nuget package; https://www.nuget.org/packages/W3S-GCS/ 
<br />
```
Install-Package W3S-GCS
```

#### Umbraco package
...

### How to configure this plugin?
Please follow the steps below to set-up a fully functioning GCS plugin.

#### 0. Add the GCS section to a user group.
You will need to allow your current Umbraco User access to the GCS section via the Umbraco backoffice. You can find these settings at the Users section via the left navigational panel.

#### 1. Create a search page
GCS needs a page to display the search results. Therefore create a new document type with template in the umbraco backoffice.

#### 2. Back office configuration
After you've installed this plugin some mandatory configuration needs to be performed. 
In the backoffice navigate to the GCS section and configure the following fields:

- `Base URL`          > https://www.googleapis.com/customsearch/v1
- `CX Key`            > The custom search engine ID to use for this request. Go to https://cse.google.com/all to create a new search engine and retrieve the token id.
- `API Key`           > JSON/Atom Custom Search API requires the use of an API key. Go to https://console.developers.google.com/apis/credentials to create an API key or to retrieve one.
- `Redirect alias`    > Enter the document type alias of the search results page.
- `Development URL`   > When working on a environment other than the production environment enter the absolute (including scheme) indexed domain name.

Other configuration fields are optional. For each field a short description of it's functionality is given below the label.

#### 3. Dependencies 
You'll  need to reference the .js file that come alongside this package. 

Add a reference to the following script file either in a _layout.cshtml file or in a bundles file.
```
<script src="~/App_Plugins/W3S_GCS/Scripts/gcsearch.min.js" type="text/javascript"></script>
```
<br />

#### 4. Templates
A basic installation only needs two html elements to be inserted in your template.



#### 5. Cultures & hostnames 
The plugin requires the domain names to be configured at the top level (homepage) of the content tree. 

#### 6. Styling 
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


#### Search input
To allow a user to search throughout your website you will need to add a input field on any desired page.
Copy the following snippet anywhere in one of your templates to add the input field. 

```
<input type="text" class="gcs gcs_input" name="text" placeholder="">
```
<br />

#### Search results
Secondly an element where the search results will be appended to needs to be iserted on your search results page.
Copy the following snippet on your search results page.

```
<div class="gcs gcs_results"></div>
```

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
<div class="gcs gcs_pagination"></div>
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

### Running things locally
1. Download (or fork) this project to your local machine.
2. Open the .sln file with the Visual Studio IDE and build the project.
3. Set-up a website in IIS and let the physical path point to the /Umbraco7 folder.
4. Navigate to the URL you've configured (in bindings).
5. Navigate to /umbraco and login using;
    - username: info@w3s.nl
    - password: googlecustomsearch
6. The GCS section was already added to the administrator user group so you can navigate to the GCS section via the left nav panel.
7. Navigate to `Settings` > tab `auth set-up` where you'll find some mandatory fields to fill in.
8. `CX Key`: Create a new search engine via https://cse.google.com/all (Custom Search Engine ID). You'll find your Search engine id / cx key beneath menu item Setup.
9. `API Key`: Create an API key via https://console.developers.google.com/apis/credentials. Activate the custom Search API via menu item Library.
10. Navigate to the root of the website where a seach input field will appear and try any search query.

The other fields that you'll find are preconfigured for the sake of this demo.
Please skip to `How to configure this plugin?` below for more specific information on this topic.
