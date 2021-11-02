# Nash_IRL

Nash_IRL is a lightweight social meetup application designed to foster easier event organization for groups of friends. It was designed to answer the problem of organizing social events among my classmates - rather than have disparate communication across multiple social platforms, I envisioned a bloat-free one-stop application for planning get-togethers organized by interests. A user has a timeline of upcoming events displayed right on the homepage, and they may select hobbies to display that hobby's events and post their own. Each event has file uploads for a relevant landing page image for the event and full message board functionality for dialog between potential attendees.

## Required Resources

This proof-of-concept for Nash_IRL is rendered server-side in an ASP.NET MVC application, utilizing Google Firebase for user authentication and Cloudinary API for image hosting. To run this proof-of-concept locally, the user will need [Microsoft Visual Studio](https://visualstudio.microsoft.com/) to serve up the project solution. They should run the included DB table creation script on Visual Studio or the SQL management software of their choice to create a local SQL database. They will also need to set up a matching project on [Firebase](https://firebase.google.com/). They should select Email/Password authentication on Firebase, and they will need to include the connection string for their SQL database and a FirebaseApiKey, obtained from their Firebase dashboard.

> NOTE: It is HIGHLY RECOMMENDED that you create a .gitignore file in your root project directory and ignore your appsettings.json file. Exposing private api keys in public repositories is bad practice and can lead to serious security vulnerabilities.

The user will also need a Cloudinary account. Their account will include an api key, api secret key, and cloud name. In their appsettings.json file, they should include this JSON object:

```
"AccountSettings": {
    "CloudName": "<your cloud name here>",
    "ApiKey": "<your API key here>",
    "ApiSecret": "<your secret key here>",
    "ApiBaseAddress": "https://api.cloudinary.com"
  }
```
## To Run application:

Run the application in Visual Studio. It will serve up the app on ports 5000/5001. Register at least one new user to be authenticated and use the app.

## ERD

[Tentative data mapping for Nash_IRL](https://dbdiagram.io/d/61662c41940c4c4eec921045)

## WireFrame

Initial layout design for Nash_IRL:

[Wireframe designed in Miro](https://miro.com/app/board/o9J_lpwXapY=/)

##Acknowledgements

This app would not have been possible without the incredible instructors and staff at [Nashville Software School](https://nashvillesoftwareschool.com/). I have never failed to be impressed by their tireless efforts to make us better developers, or moved by their unfailing generosity to me during my time at this school. Special thanks to Josh Barton, my mentor, scrum leader, sounding board, and friend for this capstone. 
