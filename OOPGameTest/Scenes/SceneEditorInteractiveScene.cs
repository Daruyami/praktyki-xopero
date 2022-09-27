using System;

namespace OOPGameTest
{
    public class SceneEditorInteractiveScene : InteractiveScene
    {
        public SceneEditorInteractiveScene(string name = "Scene Editor",
            string description = "This is an Engine now!", bool persistence = true, bool isPopup = false,
            IScene[] options = null) : base(name, description, persistence, isPopup, options) { }
        
        protected override void InitOptions()
        {
            //tworzenie/zapisywanie/załadowywanie scen z jsona
            /*
             * >instancja jakiejś klasy (jakiejś generic klasy? jakiejś specyficznej klasy?)
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
             * 
             */
            
            /*var n = GetFirstAvailableOptionIndex();
            Console.Out.WriteLine("N: "+n);
            Options[n] = this;*/
        }
    }
}