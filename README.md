# W3S GCS
Google Custom Search plugin for Umbraco v7.x

### About
This project was started since Google has announced that they will discontinue their Site Search service as of april 2018. As an alternative this plugin for Umbraco uses the Google Custom Search API to retrieve search results and display them on a page.

### Set-up a DEMO
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

The other fields that you'll find are preconfigured in this demo.
Please skip to `Configuration` > `Back office` in this readme for more information.

### Installation
#### Nuget 
This plugin will is available via Nuget; https://www.nuget.org/packages/W3S-GCS/
Run ```Install-Package W3S-GCS```

### Configuration
#### Back office 
After you've installed this plugin some mandatory configuration needs to be performed. 
In the backoffice navigate to the GCS section.

- `Base URL`          > https://www.googleapis.com/customsearch/v1
- `CX Key`            > The custom search engine ID to use for this request. Go to https://cse.google.com/all to get the token.
- `API Key`           > JSON/Atom Custom Search API requires the use of an API key. Go to https://console.developers.google.com/apis/credentials to create an API key or to retrieve one.
- `Redirect alias`    > Enter the document type alias of the search results page.
- `Development URL`   > When working on a environment other than the production environment enter the absolute (including scheme) indexed domain name.

### Basic Installation
A basic installation only needs two html elements to be inserted in your template alongside some classes.
You'll also need to reference the .js file that come alongside this package. 

<br />

#### Scripts
Add a reference to the following script file either in a _layout.cshtml file or in a bundles file.
```
<script src="~/App_Plugins/GCS/Scripts/gcsearch.min.js" type="text/javascript"></script>
```
<br />

#### Search input
To allow a user to search throughout your website you will need to add a input field on any desired page.
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

<br />
