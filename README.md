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
