# Elasticsearch + Kibana + WebApi Proof Of Concept
This demo should showcase how to configure and index documents (PDF, DOC, DOCX, etc) using Elasticsearch instance.

Web API is an interface to search thru documents and indexing.

## Setup
- Execute command ```bash setup.sh```
- Open browser and type http://localhost:5601
### Initialize pipeline
```json
PUT _ingest/pipeline/documents
{
  "description" : "Index documents content",
  "processors" : [
    {
      "attachment" : {
        "field" : "data"
      },
      "remove":{
        "field":"data"
      }
    }
  ]
}
```
### Setup index
```json
PUT /documents
{
  "mappings": {
    "properties": {
      "attachment.content": {
        "type": "text",
        "analyzer": "english"
      }
    }
  }
}
```
- Execute script ```bash data-load.sh```

### Search data
```json
POST /documents/_search
{
  "query": {
    "query_string": {
      "default_field": "attachment.content", 
      "query": "Godaddy"
    }
  }
}
```