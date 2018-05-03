# W3S GCS
Google Custom Search plugin for Umbraco v7.x

### About
This project was started since Google has announced that they will discontinue their Site Search service as of april 2018. As an alternative this plugin for Umbraco uses the Google Custom Search API to retrieve search results and display them on a page.

### Set-up a DEMO

Visit https://w3sgcs.w3s.nl for a live demo.

#### Setings things up locally
1. Download (or fork) this project to your local machine.
2. Open the .sln file with the Visual Studio IDE and build the project.
3. Set-up a website in IIS and let the physical path point to the /Umbraco7 folder.
4. Navigate to the URL you've configured (in bindings).
5. Navigate to /umbraco and login using;
    username: info@w3s.nl
    password: googlecustomsearch
6. The GCS section is already added to the administrator user group. 
   Navigate to the GCS section via the left nav panel.
7. Navigate to `Settings` > tab `auth set-up` where you'll find some mandatory fields to fill in.

Mandatory fields:
CX Key:
- Create a new search engine via https://cse.google.com/all (Custom Search Engine ID). Do not forget to add the sites to search.

API Key:
- Create a API key https://console.developers.google.com/apis/credentials

- Navigate to the root of the website where a seach input field will appear. 
- Try any search query...

The other fields are preconfigured just to set-up a working demo.
Please skip to Configuration > Back office to get more information about the other types of configuration.

### Installation
#### Nuget 
This plugin will is available via Nuget; https://www.nuget.org/packages/W3S-GCS/

### Configuration
#### Back office 
Before you begin to use this plugin some configuration needs to be performed. 
In the backoffice navigate to the GCS section tab settings. 

- Base URL        >
- CX Key          >
- API Key         > 
- Redirect alias  >
- Development URL >

#### Back-end / front-end
The documentation below can also be reached from the GCS section in the backoffice or in via the source.

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

### Umbraco login
Login in the Umbraco backoffice with the following credentials:

username: info@w3s.nl
password: googlecustomsearch
