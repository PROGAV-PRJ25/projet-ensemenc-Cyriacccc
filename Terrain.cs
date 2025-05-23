 /*Classe Terrain - chaque potager est associé à un type de terrain*/
    class Terrain
    {
        public string Type { get; set; } /*Le type de sol : Terre, Argile, Sable...*/
        public double Espacement { get; set; } /*Distance nécessaire entre les plantes sur ce terrain*/
        public int Capacite { get; set; } /*Nombre maximum de plantes que ce terrain peut accueillir*/

        public Terrain(string type, double espacement, int capacite)
        {
            Type = type;
            Espacement = espacement;
            Capacite = capacite;
        }

        /*Affiche les caractéristiques actuelles du terrain à l'écran, utile à chaque changement de terrain*/
        public void AfficherCaracteristiques()
        {
            Console.WriteLine($"--- TERRAIN ACTUEL ---\nType: {Type} | Espacement: {Espacement} | Capacité: {Capacite}\n");
        }
    }

