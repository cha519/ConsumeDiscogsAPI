# ConsumeDiscogsAPI
A simple .NET Core MVC Web Application for demonstrating the consumption of the Discogs API.

Discogs API Documentation found [here](https://www.discogs.com/developers/).

## Quirks/Concerns/TODO
- Discogs API requests that the developer provide a specifc format for the User-Agent HTTP header string (required or the request is 403 Forbidden). .NET wouldn't parse this full string so a partial version of that header is provided and currently works. Discogs warns that it may quietly block apps who don't provide the full header.   
