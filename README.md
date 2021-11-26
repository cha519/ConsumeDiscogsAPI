# ConsumeDiscogsAPI
A simple .NET Core MVC Web Application for demonstrating the consumption of the Discogs API.

Discogs API Documentation found [here](https://www.discogs.com/developers/).

## Features
- "Chris-cogs" feature: allows the user (e.g. my partner, Katy) to peruse my current wantlist of vinyl in via this easy-to-access, single page summary site (if ever at a loss for Xmas, Birthday (or any other notable event) presents).      

## Quirks/Concerns/TODO
- Discogs API requests that the developer provide a specifc format for the User-Agent HTTP header string (required or the request is 403 Forbidden). .NET wouldn't parse this full string so a partial version of that header is provided and currently works. Discogs warns that it may quietly block apps who don't provide the full header.   
- I'm aware that much of code could/should be abstracted out of the controller into a service in a hypothetical, business layer, but time constraints...