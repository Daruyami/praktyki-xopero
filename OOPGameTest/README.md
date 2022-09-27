## TODO

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
