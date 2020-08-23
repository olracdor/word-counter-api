# Cloud Word Counter

The cloud word count is the number of words in a HTML document. Word counting may be needed when a text is required to stay within certain numbers of words.

## Getting Started

Assuming we've freshly checkout the source, make sure you've installed
already the following tools. (Don't run the commands when you've already 
done it before)

### Install Global Tools

* Install git
    * https://git-scm.com/book/en/v2/Getting-Started-Installing-Git

* Download and install dotnet SDK
    * https://dotnet.microsoft.com/download

* Download and install docker 
    * Windows - https://docs.docker.com/docker-for-windows/install/
    * Ubuntu - https://docs.docker.com/engine/install/ubuntu/
    * Mac - https://docs.docker.com/docker-for-mac/install/

* Clone Word Counter API 
    * git clone https://github.com/olracdor/word-counter-api.git
    
### Build the API

On the root folder, run below commands in terminal/bash

Build the dotnet API 

```bash
dotnet publish -c Release
```

Build the docker image 

```bash
docker build -t  <YOUR CONTAINER REGISTRY NAME>/word-counter-docker .
```
**Azure container registry sample**

```bash
docker build -t  wordcounter.azurecr.io/word-counter-docker .
```

## Run and Test

Run the docker image locally

```bash
docker run -p <PREFERED PORT>:80 <YOUR CONTAINER REGISTRY NAME>/word-counter-docker
```

**Sample Command**
```bash
docker run -p 5001:80 wordcounter.azurecr.io/word-counter-docker
```

### Testing the API

**Assuming you're using port 5001** - you can test the newly created docker image using below commands.

Count words of a website
```bash
curl --request POST \
  --url http://localhost:5001/api/v1/words/count \
  --header 'content-type: application/json' \
  --data '{
    "url": "https://docs.microsoft.com/en-us/dotnet/standard/collections/"
}'
```

**Sample Response**

```json
[
  {
    "key": "the",
    "word": "THE",
    "count": 75,
    "average": 8
  },
  {
    "key": "and",
    "word": "AND",
    "count": 37,
    "average": 8
  },
  {
    "key": "collection",
    "word": "COLLECTION",
    "count": 33,
    "average": 8
  },
  {
    "key": "of",
    "word": "OF",
    "count": 32,
    "average": 8
  }
]
```

Get all recorded word counts
```bash
curl --request GET \
  --url http://localhost:5001/api/v1/words/count
```

**Sample Response**

```json
[
  {
    "word": "the",
    "count": 75,
    "partitionKey": "https:--docs.microsoft.com-en-us-dotnet-standard-collections-",
    "rowKey": "3432e941-aca6-4fe1-afec-4f4601e1ee00",
    "timestamp": "2020-08-22T20:22:39.188809+00:00",
    "eTag": "W/\"datetime'2020-08-22T20%3A22%3A39.188809Z'\""
  },
  {
    "word": "and",
    "count": 37,
    "partitionKey": "https:--docs.microsoft.com-en-us-dotnet-standard-collections-",
    "rowKey": "69a13b3f-1808-4757-8516-aad6ebf9d08e",
    "timestamp": "2020-08-22T20:22:39.188809+00:00",
    "eTag": "W/\"datetime'2020-08-22T20%3A22%3A39.188809Z'\""
  },
  {
    "word": "collection",
    "count": 33,
    "partitionKey": "https:--docs.microsoft.com-en-us-dotnet-standard-collections-",
    "rowKey": "7179c0a4-ff60-4774-850a-65855651801b",
    "timestamp": "2020-08-22T20:22:39.188809+00:00",
    "eTag": "W/\"datetime'2020-08-22T20%3A22%3A39.188809Z'\""
  },
  {
    "word": "of",
    "count": 32,
    "partitionKey": "https:--docs.microsoft.com-en-us-dotnet-standard-collections-",
    "rowKey": "90ce0827-824c-4cdc-9b9f-0eb0267d5fde",
    "timestamp": "2020-08-22T20:22:39.188809+00:00",
    "eTag": "W/\"datetime'2020-08-22T20%3A22%3A39.188809Z'\""
  }
]
```

## Live Word Counter

### Using docker and Azure App Service.

Count words of a website:
```bash
curl --request POST \
  --url https://word-counter.azurewebsites.net/api/v1/words/count \
  --header 'content-type: application/json' \
  --data '{
    "url": "https://docs.microsoft.com/en-us/dotnet/standard/collections/"
}'
```

### Get all recorded word counts
```bash
curl --request GET \
  --url https://word-counter.azurewebsites.net/api/v1/words/count
```

## API Definition

```json
https://word-counter.azurewebsites.net/swagger/v1/swagger.json
```