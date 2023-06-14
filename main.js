require('dotenv').config();
const { MongoClient, ObjectID } = require("mongodb");
const Express = require("express");
const BodyParser = require('body-parser');
const server = Express();

server.use(BodyParser.json());
server.use(BodyParser.urlencoded({ extended: true }));


DBUSER = process.env.DBUSER;
DBPASSWORD = process.env. DBPASSWORD;


const uri = "mongodb+srv://" + DBUSER + ":" + DBPASSWORD + "@cluster0.u52w3.mongodb.net/MENU?retryWrites=true&w=majority";
const client = new MongoClient(uri);

var collection;

server.listen("3000", async () => {
    try {
        await client.connect();
        collection = client.db("plummeting-people").collection("plummies");
        collection.createIndex('plummie_tag');
    } catch (e) {
        console.error(e);
    }
});

server.post("/plummies", async (request, response, next) => {
    try {
        let result = await collection.insertOne(request.body);
        response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.get("/plummies/:plummie_tag", async (request, response, next) => {
    try {
        let result = await collection.findOne({ "plummie_tag": request.params.plummie_tag });
        response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.get("/plummies", async (request, response, next) => {
    try {
        let result = await collection.find({}).toArray();
        response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.put("/plummies/:plummie_tag", async (request, response, next) => {
    try {
        let result = await collection.updateOne(
            { "plummie_tag": request.params.plummie_tag },
            { "$set": request.body }
        );
        response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.delete("/plummies/:plummie_tag", async (request, response, next) => {
    try {
        let result = await collection.deleteOne({ "plummie_tag": request.params.plummie_tag });
        response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});