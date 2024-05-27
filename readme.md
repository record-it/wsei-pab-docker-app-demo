# DocApp
Przykładowa aplikacja z konfiguracją kontenera Docker.
Gotowy kontener zawiera:
- aplikację
- silnik bazodanowy PostgreSQL
- zestaw skryptów inicjujących bazę z danymi
Pliki i katalogi:
- Dockerfile - plik opisujący tworzenie obrazu aplikacji na podstawie dwóch obrazów .NET (ASPNET i SDK)
- docker-compose.yml - plik opisujący tworzenia obrazu zawierającego 
  - obraz aplikacji
  - obraz silnika bazy danych ze skryptami
- sqlscripts - katalog skrytów sql

## Budowa i uruchomienie
Z poziomu katalogu projektu DocApp w terminalu należy wpisać:
```text
  docker-compose up --build
```
Aplikacja pracuje pod portem 8080:
```http request
http://localhost:8080/api/books/1234
```