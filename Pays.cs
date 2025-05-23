 /*Classe Pays - ici on gère tout ce qui est météo et saisons en fonction du pays choisi*/
    class Pays
    {
        public string Nom { get; } /*Le nom du pays (ici c'est le Brésil)*/
        private List<string> Saisons; /*Liste des saisons possibles dans ce pays*/

        public Pays(string nom)
        {
            Nom = nom;
            Saisons = new List<string> { "Saison sèche", "Saison des pluies" }; /*Le Brésil a ces deux saisons principales*/
        }

        /*Cette méthode choisit une saison aléatoirement pour débuter ou après 15 semaines*/
        public string GetSaisonAleatoire() => Saisons[new Random().Next(Saisons.Count)];

        /*On génère une température réaliste selon la saison (saison sèche = plus chaud, saison des pluies = plus frais)*/
        public double GenererTemperatureSelonSaison(string saison) =>
            saison == "Saison sèche" ? new Random().Next(15, 31) : new Random().Next(0, 16);

        /*L'humidité aussi dépend de la saison : saison des pluies = très humide !*/
        public double GenererHumiditeSelonSaison(string saison) =>
            saison == "Saison sèche" ? new Random().Next(40, 70) : new Random().Next(70, 100);

        /*Et pareil pour la luminosité : plus de soleil en saison sèche, plus nuageux pendant les pluies*/
        public double GenererLuminositeSelonSaison(string saison) =>
            saison == "Saison sèche" ? new Random().Next(60, 100) : new Random().Next(30, 60);
    }