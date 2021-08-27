# .NET II Final Project: WhereToWatch v3.0
WhereToWatch v3.0 is a media search tool that I made for my .NET II Final Project in Jim Glasgow's class at Kirkwood Community College. 

During this semester we learned more C#, more visual studio, using a database with our program, and integrating ASP.NET into our project so our desktop program has and integrates with our website. 

I rewrote a ton of my old code from the previous project and redid all the interfaces in xaml with a new style. The tool itself allows you to search up any movie or tv show you can think of, and be provided various details about the result, and even a trailer link, if available. It accomplishes this by communicating with TheMovieDB's API.

# IMPORTANT STEPS FOR TESTING
1. Add your API Key to MediaLogicLayer/MediaAPI.cs
![image](https://user-images.githubusercontent.com/8385256/131178628-40ef385f-60a4-4c52-ad41-753fbd17025d.png)
3. Add the name of your SQL Server Instance to each sqlcmd command in create_db.bat (the default is sqlexpress)
![image](https://user-images.githubusercontent.com/8385256/131178544-7ef6f7c1-0aaa-424f-adfa-452a7033b6fc.png)
4. Add the name of your SQL Server Instance to the _connectionString in MediaDataAccess/DBConnection.cs
![image](https://user-images.githubusercontent.com/8385256/131178454-f28fee3d-8dc9-4980-8f3a-7f3ee367364d.png)
5. Switch the current startup project between WpfPresentation(DESKTOP APP) and WebPresentation(WEBSITE), or set both of them to start in Multiple startup projects.
![image](https://user-images.githubusercontent.com/8385256/131178378-ba2e5b55-efff-42ce-acdf-8049b1b1d601.png)



# Usage/Features
1. First we run the create_db.bat file, this file will run all our SQL scripts and create our stored procedures and test data.
![image](https://user-images.githubusercontent.com/8385256/131170776-d4299dab-7f21-4d81-96cc-ab605b36d988.png)
![Animation](https://user-images.githubusercontent.com/8385256/131170844-579a420f-e708-4c95-8433-8491b92928ed.gif)
2. We open the landing page.
![image](https://user-images.githubusercontent.com/8385256/131174470-dbe207f9-21dd-4e4e-be71-6da99b54bf98.png)
3. We search for any movie or tv show.
![image](https://user-images.githubusercontent.com/8385256/131174545-6ffea5e7-e716-46c5-b98d-142848e28b44.png)
4. The result page of an item.
![image](https://user-images.githubusercontent.com/8385256/131174607-f92f32be-7917-4758-9c86-a4d7ac6dc0b5.png)
5. We login or register for an account if we don't have one.
![image](https://user-images.githubusercontent.com/8385256/131174674-2dd2cdf6-6e05-455c-9962-bd0b6478457e.png)
6. We have successfully registered, and have been given a default password of "newuser", which the system will require us to change upon first login.
![image](https://user-images.githubusercontent.com/8385256/131174758-60176536-0a21-4650-884a-3c4427fb2b2e.png)
![image](https://user-images.githubusercontent.com/8385256/131174804-878b1a30-b277-479c-8a96-edf33f1eb813.png)
![image](https://user-images.githubusercontent.com/8385256/131174819-7256fafe-79d2-43fc-91b7-9be652ef00f7.png)
7. We can now add an item to our favorites list, which is now stored in the database.
![image](https://user-images.githubusercontent.com/8385256/131174879-06fa9015-a202-49b9-8dad-e7c8366e1839.png)
8. We can now display our favorites list within the program, the results being loaded from the database.
![image](https://user-images.githubusercontent.com/8385256/131175164-b09a189d-ab8f-44b9-9bfa-9813444a6bad.png)
9. Switching over to the web portion, we are presented with the landing page.
![image](https://user-images.githubusercontent.com/8385256/131176078-ade891f8-c3f4-4168-a849-e1539c89127c.png)
10. We can view all movies and tv-shows in the database.
![image](https://user-images.githubusercontent.com/8385256/131176310-8d31fec5-6a77-4a6c-95f3-8d94b34a6d01.png)
![image](https://user-images.githubusercontent.com/8385256/131176345-f17030ae-2409-43c5-baad-ed1120e8c7f4.png)
11. For all movies in the DB, we can view their details, edit their details, and delete them from the DB.
![image](https://user-images.githubusercontent.com/8385256/131176565-d978f8c4-8dd4-40da-9580-5a0620f759d8.png)
![image](https://user-images.githubusercontent.com/8385256/131176593-08b422c2-9f19-4fc9-9251-c9f1689ca1af.png)
![image](https://user-images.githubusercontent.com/8385256/131176674-dc857b21-72f7-41cb-8cad-d34806d3e86b.png)
12. Trending Movies and Trending TV Shows
![image](https://user-images.githubusercontent.com/8385256/131177070-7434cb3d-ff05-436b-b3c1-4517041e1885.png)
![image](https://user-images.githubusercontent.com/8385256/131177052-27815ada-4489-4834-a237-df1d2c86bf9a.png)
13. We register for an account or login if we already have one.
![image](https://user-images.githubusercontent.com/8385256/131177303-d8ceee2a-3b65-47ce-9027-c204e51e406f.png)
14. We can now view our favorites, which has our tv-show that we favorited on the desktop app. Here we can delete it, or add more items to our favorites, either through the desktop app or through the web portion.
![image](https://user-images.githubusercontent.com/8385256/131177816-28978432-4806-4d5c-9af2-2edac5b9546f.png)

