    require('dotenv').config();
    const { MongoClient, ObjectID } = require("mongodb");
    const Express = require("express");
    const BodyParser = require('body-parser');
    const mongoose = require("mongoose");
    const { append } = require("express/lib/response");
    const server = Express();

    server.use(BodyParser.json());
    server.use(BodyParser.urlencoded({ extended: true }));

    DBUSER = process.env.DBUSER;
    DBPASSWORD = process.env. DBPASSWORD;


    
    /*
    mongoose.connect(`mongodb+srv://${DBUSER}:${DBPASSWORD}@cluster0.u52w3.mongodb.net/MENU?retryWrites=true&w=majority`).then(()=>{
        server.listen('3000', ()=>{
            console.log("listening at 3000")
        })
    }).catch(error=>{
        console.log("cannot connect to db" + error)
    })
    */
    var collection;
    /*
    server.post("/plummies", async (request, response, next) => {});
    server.get("/plummies", async (request, response, next) => {});
    server.get("/plummies/:id", async (request, response, next) => {});
    server.put("/plummies/:plummie_tag", async (request, response, next) => {});
    */

    async function listDatabases(client){
        databasesList = await client.db().admin().listDatabases();
     
        console.log("Databases:");
        databasesList.databases.forEach(db => console.log(` - ${db.name}`));
    };

    async function main() {
        const uri = "mongodb+srv://" + DBUSER + ":" + DBPASSWORD + "@cluster0.u52w3.mongodb.net/MENU?retryWrites=true&w=majority";
        const client = new MongoClient(uri);

        try {
            // Connect to the MongoDB cluster
            await client.connect();
     
            // Make the appropriate DB calls
            await  listDatabases(client);
     
        } catch (e) {
            console.error(e);
        } finally {
            await client.close();
        }
        /*server.listen("3000", async () => {
            try {
                await client.connect();
                collection = client.db("plummeting-people").collection("plummies");
                console.log("Listening at :3000...");
            } catch (e) {
                console.error(e);
            }
        });*/
    }
    
    main().catch(console.error);