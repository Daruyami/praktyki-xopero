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
- Scent typu popup (zachowuje poprzednie okienko (rodzica) - `Enter()` returnuje true)
- globalna(?) klasa gracza, scena z statystykami, scena z eq
  - czy dostęp do inputu aby wyświetlić sceny graczowe jest globalny (tj tylko kiedy masz gracza jakiegoś)
- New/Load game - zapisy gry