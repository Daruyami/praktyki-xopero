using System;
using System.IO;
using Newtonsoft.Json;

namespace OOPGameTest
{
    //przypuszczam że będzie ta klasa tymczasowa póki nie zrobie ogólnej klasy dla lokacji
    public class TownInteractiveScene : InteractiveScene 
    {
        public TownInteractiveScene(string name = "an Unnamed Town", string description = "", bool persistence = true,
            bool isPopup = false, IScene[] options = null) : base(name, description, persistence, isPopup, options) { }
        
        protected override void InitOptions() { }

        protected override void Exit() { } //zoverridowany Exit() tutaj?

        
    }
}