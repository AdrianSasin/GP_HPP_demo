# GP HPP Console Demo

Prosta aplikacja konsolowa w .NET, która integruje się z Global Payments / eService Gateway i umożliwia utworzenie linku do płatności (Hosted Payment Page).

Aplikacja:
- pobiera kwotę z konsoli
- tworzy Pay By Link (HPP)
- zwraca URL do płatności
- wyświetla link w konsoli (opcjonalnie można otworzyć w przeglądarce)

Wymagania

- .NET 8 SDK
- Visual Studio 2022 lub nowszy (lub VS Code)
- konto testowe Global Payments / eService Gateway
- dane dostępowe:
  - AppId
  - AppKey

Instalacja

git clone https://github.com/TWOJ_LOGIN/TWOJE_REPO.git
cd TWOJE_REPO

dotnet restore

Projekt używa paczki:
GlobalPayments.Api

Konfiguracja

Aplikacja nie przechowuje danych dostępowych w kodzie.

Przed uruchomieniem ustaw zmienne środowiskowe.

Windows PowerShell:
setx GP_APP_ID "twoj_app_id"
setx GP_APP_KEY "twoj_app_key"

Windows CMD:
setx GP_APP_ID "twoj_app_id"
setx GP_APP_KEY "twoj_app_key"

Po ustawieniu zmiennych zamknij i uruchom ponownie terminal lub Visual Studio.

Uruchomienie

dotnet run

lub w Visual Studio uruchom projekt (F5).

Jak działa aplikacja

Po uruchomieniu aplikacja poprosi o kwotę:

Podaj kwotę (PLN):

Wpisz np.:
10

Po naciśnięciu Enter:
- tworzony jest Pay By Link (HPP)
- aplikacja zwraca URL do płatności
- link można otworzyć w przeglądarce i dokończyć płatność

Endpoint

Aplikacja korzysta ze środowiska testowego:
https://apis.sandbox.eservicegateway.com/ucp

Bezpieczeństwo

Nie commituj:
- AppId
- AppKey

Dodaj do .gitignore:
bin/
obj/
*.user
*.suo
.env
appsettings.Development.json

Jeśli klucz został opublikowany w repozytorium, należy wygenerować nowy.

Typowe problemy

Brak GP_APP_ID lub GP_APP_KEY
- zmienne środowiskowe nie są ustawione lub terminal nie został zrestartowany

403 Forbidden
- błędne dane dostępowe
- dane z innego środowiska
- brak dostępu do HPP / Pay By Link
- nieprawidłowy endpoint

Brak Payment URL
- konto nie ma aktywnej usługi Pay By Link
- odpowiedź API nie zawiera PayByLinkResponse

Cel projektu

Projekt demonstracyjny pokazujący integrację Hosted Payment Page (Pay By Link) w aplikacji konsolowej .NET.
