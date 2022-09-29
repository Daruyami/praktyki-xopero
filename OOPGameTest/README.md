# Przemyślenia co do projektu

Sama podstawa projektu tj. "silnik scen" mogłaby teoretycznie służyć jako właśnie odrębny projekt silnika scen którego wtedy mógłbym używać do innych projektów jako prostego tui. Jednak aby to osiągnąć silnik ten wymaga dodatkowych prac, przypuszczam że wymagane będzie rozwiązanie paru problemów czy implementacja nowych funkcji, te oznaczyłem w TODO gwiazdką.

## TODO

- [*] nieinteraktywne sceny typu input

- [*] implementacja obsługi akcji przez sceny (`Act()`) tak żeby działały i były proste w użytku

- Asset loader 
    - json serializacja/deserializacja
    - zdefiniowane assety:
      ``` 
      location 
      { 
          nazwa, 
          typ, 
          opcje -> npc 
          { 
              nazwa, 
              typ/klasa -> opcje (
                  dialog, quest, interakcje specyficzne dla klasy/typu, trade, walka) 
          } 
      }
      ```
    - save gracza/świata
  -     * >instancja jakiejś klasy (jakiejś generic klasy? jakiejś specyficznej klasy?)
        * >(załadowanie klasy z pliku)
        * >
        * >modyfikowanie klasy [name, description]
        * >
        * >(((dodanie obsługi akcji, predefiniowane akcje? klasa akcji à la klasa scen? kreator akcji?)))
        * >
        * >dodawanie opcji do Options
        * >instancja jakiejś klasy
        * >(załadowanie jakiejś klasy z pliku)
        * >(dodanie opcji w dziecku; goto dodawanie opcji)
        * >modyfikacja i wyjście z edycji klasy dziecka
        * >
        * >zapis zmodyfikowanej klasy (w wybranej ścieżce w ../../Assets czy coś)

- globalna(?) klasa gracza, scena z statystykami, scena z eq
  - czy dostęp do inputu aby wyświetlić sceny graczowe jest globalny (tj tylko kiedy masz gracza jakiegoś)
- New/Load game - zapisy gry
