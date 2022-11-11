# DocumentDbDemo 

This is a demo app showcasing a REST API for storing and manipulating arbitrary documents. Description of the task is included in the file TaskDescription.txt.  

Made by Tomáš Kos.

 ## Notes:
 - Post, Put and Get requests are implemented as specified in task description  
 - MongoDB is used as storage as it seemed fitting for simply storing documents and quickly retrieving them  
 - Additional output formats can be added by simply adding different output formatters in Program.cs. Some formats also require their attributes in the models.  
 - Different storage methods can be easily added by implementing the IDocumentService interface and registering in DI.  
 - Controller has unit tests, service handling MongoDB hasn't. I would probabably test the service too if it was actually going to production. It could be quite easily done by making a mock implementation of MongoDB.Driver.IMongoCollection.  

 ## MongoDB Setup
 For the app to run, it needs a connection to MongoDB containing a database and a collection for it to use. Connection string, database name and collection name need to be specified in appsettings.json.

