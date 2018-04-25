# GCS
Google Custom Search plugin for Umbraco v7.x

### About
This project was started since Google has announced that they will discontinue their Site Search service as of april 2018. As an alternative this plugin for Umbraco uses the Google Custom Search API to retrieve search results and display them on a page.

### Installation
#### Nuget 
This plugin will be made available via Nuget.

### Configuration
#### Back office 
Before you begin to use this plugin some configuration needs to be performed. In the backoffice navigate to the GCS section tab settings. 

Base URL        > Usually this will be https://www.googleapis.com/customsearch/v1
CX Key          >
API Key         > 
Redirect alias  >
Development URL >


#### Back-end / front-end
The documentation below can also be reached from the GCS section in the backoffice or in via the source.

### Basic Installation
A basic installation only needs two html elements to be inserted in your template alongside some classes.
You'll also need to reference the .js file that come alongside this package. 

GCS also creates some tables in the Umbraco database because it stores it's settings there.
<br />

#### Scripts
The .js files are automatically copied to your project by utilizing the post-build actions. Add a reference to the following script file either in a _layout.cshtml file or
in a bundles file.
```
<script src="~/App_Plugins/GCS/Scripts/gcsearch.js" type="text/javascript"></script>
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


### Running locally
- Download or fork this project.
- Open the .sln file with the Visual Studio IDE. 
- Set-up a website in IIS and let it point to the /Umbraco7 folder.
- Navigate to the development URL you've configured and check if everything works. 
- Navigate to /umbraco to login (use the credentials as stated below).
- Add the GCS section to a desired user group

### Umbraco login
Login in the Umbraco backoffice with the following credentials:

username: info@w3s.nl
password: googlecustomsearch
