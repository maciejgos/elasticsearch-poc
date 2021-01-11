#!/bin/bash
docker-compose up -d

(echo -n '{"filename":faktura_3_2020_31-12-2020.pdf", "data": "'; base64 ./data/faktura_3_2020_31-12-2020.pdf; echo '"}') | curl -H "Content-Type: application/json" -d @-  http://localhost:9200/documents/_doc/1?pipeline=documents
(echo -n '{"filename":"zbiorczyWydrukZa_grudzien_2020.pdf", "data": "'; base64 ./zbiorczyWydrukZa_grudzien_2020.pdf; echo '"}') | curl -H "Content-Type: application/json" -d @-  http://localhost:9200/documents/_doc/2?pipeline=documents
(echo -n '{"filename":zbiorczyWydrukZa_listopad_2020.pdf", "data": "'; base64 ./"data/zbiorczyWydrukZa_listopad_2020.pdf"; echo '"}') | curl -H "Content-Type: application/json" -d @-  http://localhost:9200/documents/_doc/3?pipeline=documents