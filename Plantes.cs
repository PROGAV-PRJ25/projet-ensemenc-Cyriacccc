/*Classe Plante - chaque plante a ses préférences et ses vulnérabilités*/
    class Plante
    {
        public string Nom { get; set; } /*Nom de la plante (ex: Tomate, Carotte...)*/
        public string TerrainPrefere { get; set; } /*Le type de sol idéal pour cette plante*/
        public double EspacementMin { get; set; } /*Espace minimum requis entre les plants*/
        public double TempMin { get; set; } /*Température minimale supportée*/
        public double TempMax { get; set; } /*Température maximale supportée*/
        public double HumMin { get; set; } /*Humidité minimale requise*/
        public double LumiMin { get; set; } /*Luminosité minimale requise*/
        public int Sante { get; set; } = 100; /*Santé actuelle de la plante (max 100)*/
        public int EsperanceDeVie { get; set; } /*Durée de vie en semaines*/
        public string Nature { get; set; } /*Annuelle, vivace, etc.*/
        public bool Comestible { get; set; } /*Est-ce une plante que l'on peut manger ?*/
        public List<string> SaisonsSemis { get; set; } /*Saisons pendant lesquelles on peut la semer*/
        public double VitesseCroissance { get; set; } /*Vitesse à laquelle elle pousse*/
        public List<string> Maladies { get; set; } /*Liste des maladies connues*/
        public double ProbabiliteContamination { get; set; } /*Chance qu'elle tombe malade*/
        public int Rendement { get; set; } /*Nombre de fruits ou légumes produits*/

        /*Constructeur qui initialise tous les attributs d'une plante*/
        public Plante(string nom, string terrain, double espacement, double tempMin, double tempMax, double humMin, double lumiMin, int esperanceVie,
                      string nature, bool comestible, List<string> saisonsSemis, double croissance, List<string> maladies, double proba, int rendement)
        {
            Nom = nom;
            TerrainPrefere = terrain;
            EspacementMin = espacement;
            TempMin = tempMin;
            TempMax = tempMax;
            HumMin = humMin;
            LumiMin = lumiMin;
            EsperanceDeVie = esperanceVie;
            Nature = nature;
            Comestible = comestible;
            SaisonsSemis = saisonsSemis;
            VitesseCroissance = croissance;
            Maladies = maladies;
            ProbabiliteContamination = proba;
            Rendement = rendement;
        }

        /*Vérifie combien de conditions sont respectées (terrain, espacement, température, lumière, humidité)*/
        public int NombreConditionsRespectees(Terrain terrain, double temp, double lumi, double hum)
        {
            int score = 0;
            if (terrain.Type == TerrainPrefere) score++;
            if (terrain.Espacement >= EspacementMin) score++;
            if (temp >= TempMin && temp <= TempMax) score++;
            if (lumi >= LumiMin) score++;
            if (hum >= HumMin) score++;
            return score;
        }

        /*Améliore la santé de la plante, sans dépasser 100*/
        public void AugmenterSante(int points) => Sante = Math.Min(Sante + points, 100);

        /*Réduit la santé, mais elle ne descend jamais sous 0*/
        public void DiminuerSante(int points) => Sante = Math.Max(Sante - points, 0);

        /*Vérifie si la plante est morte (santé à 0)*/
        public bool EstMorte() => Sante <= 0;

        /*Propose à l'utilisateur une action pour entretenir la plante cette semaine*/
        public void EffectuerAction(bool pluie)
        {
            Console.WriteLine($"Sur {Nom}, quelle action souhaitez-vous effectuer ?\n1=Arroser\n2=Pailler\n3=Désherber\n4=Traiter\n5=Installer serre\n6=Barrière\n7=Pare-soleil\n8=Rien");
            string action = Console.ReadLine()!;

            switch (action)
            {
                case "1": /*Arroser (si pas déjà de la pluie)*/
                    if (!pluie) AugmenterSante(10);
                    else Console.WriteLine("🚿 Trop d'eau!");
                    break;

                case "2": /*Pailler*/
                case "3": /*Désherber*/
                    AugmenterSante(5);
                    break;

                case "4": /*Traiter*/
                    AugmenterSante(8);
                    break;

                case "5": /*Installer une serre*/
                    AugmenterSante(6);
                    break;

                case "6": /*Mettre une barrière*/
                    AugmenterSante(4);
                    break;

                case "7": /*Pare-soleil*/
                    AugmenterSante(3);
                    break;

                default: /*Si l'utilisateur ne fait rien : la plante s'affaiblit*/
                    DiminuerSante(5);
                    break;
            }
        }
    }