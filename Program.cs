
using System;
using System.Collections.Generic;

namespace Prog
{
    /*Classe principale qui sert à démarrer notre simulateur de potager*/
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SIMULATEUR DE POTAGER - ProgAV 2025 ===\n");

            /*On crée une instance de simulateur et on lance la simulation*/
            Simulateur simulateur = new Simulateur();
            simulateur.LancerSimulation();
        }
    }

    /*Cette classe gère toute la logique du simulateur de potager*/
    class Simulateur
    {
        /*Déclaration des champs nécessaires à la simulation*/
        private Pays pays;
        private Terrain terrain;
        private List<Terrain> terrainsDisponibles;
        private List<Plante> potager;
        private List<Plante> plantesDisponibles;
        private Random rand;
        private string saisonActuelle;
        private int semainesRestantesDansSaison;

        /*Constructeur pour initialiser tous les éléments du jeu*/
        public Simulateur()
        {
            pays = new Pays("Brésil");
            rand = new Random();
            terrain = CreerTerrainAleatoire();
            terrainsDisponibles = new List<Terrain> { new Terrain("Terre", 0.4, 20), new Terrain("Argile", 0.6, 20), new Terrain("Sable", 0.3, 20) };
            potager = new List<Plante>();

            /*Liste des plantes qu'on pourra planter*/
            plantesDisponibles = new List<Plante>
            {
                new Plante("Tomate", "Terre", 0.5, 15, 30, 60, 80, 6, "annuelle", true, new List<string>{"Saison sèche"}, 1.0, new List<string>{"mildiou"}, 0.2, 6),
                new Plante("Carotte", "Sable", 0.3, 10, 25, 50, 70, 7, "annuelle", true, new List<string>{"Saison des pluies"}, 0.8, new List<string>{"vers"}, 0.1, 4),
                new Plante("Pomme de terre", "Argile", 0.6, 5, 20, 65, 60, 6, "annuelle", true, new List<string>{"Saison des pluies"}, 1.2, new List<string>{"mildiou"}, 0.25, 8)
            };

            terrain.AfficherCaracteristiques();
            saisonActuelle = pays.GetSaisonAleatoire();
            semainesRestantesDansSaison = 15;
        }

        /*Fonction pour créer un terrain au hasard*/
        private Terrain CreerTerrainAleatoire()
        {
            List<Terrain> choix = new List<Terrain> {
                new Terrain("Terre", 0.4, 20),
                new Terrain("Sable", 0.3, 20),
                new Terrain("Argile", 0.6, 20)
            };
            return choix[rand.Next(choix.Count)];
        }

        /*Affiche l'état actuel du potager*/
        private void AfficherInventaire()
        {
            Console.WriteLine("\n---🌱 INVENTAIRE DU POTAGER ---");
            for (int i = 0; i < potager.Count; i++)
            {
                Plante p = potager[i];
                Console.WriteLine($"{i + 1}.  {p.Nom} |🧑‍⚕️ Santé: {p.Sante}/100 |💗Vie restante: {p.EsperanceDeVie}");
            }
            Console.WriteLine($"Capacité utilisée: {potager.Count}/{terrain.Capacite} (Terrain: {terrain.Type})\n");
        }

        /*Fonction principale qui simule semaine après semaine*/
        public void LancerSimulation()
        {
            int semaine = 1;

            while (true)
            {
                Console.WriteLine("\n==================================================");
                Console.WriteLine($"===📅 SEMAINE {semaine} ===");

                /*Changement de saison toutes les 15 semaines*/
                if (semainesRestantesDansSaison == 0)
                {
                    saisonActuelle = pays.GetSaisonAleatoire();
                    semainesRestantesDansSaison = 15;
                }

                /*On génère les conditions météo de la semaine*/
                double temperature = pays.GenererTemperatureSelonSaison(saisonActuelle);
                double humidite = pays.GenererHumiditeSelonSaison(saisonActuelle);
                double luminosite = pays.GenererLuminositeSelonSaison(saisonActuelle);
                bool pluie = rand.NextDouble() < 0.4;
                bool grele = rand.NextDouble() < 0.1;
                bool rongeur = rand.NextDouble() < 0.1;

                /*Affichage des conditions météo*/
                Console.WriteLine($"Saison: {saisonActuelle} |🌡️ Température: {temperature}°C | Humidité: {humidite}% |☀️ Luminosité: {luminosite}%");
                Console.WriteLine($"🌧️  Pluie: {(pluie ? "Oui" : "Non")} |🧊 Grêle: {(grele ? "Oui" : "Non")} |🐀 Rongeur: {(rongeur ? "Oui" : "Non")}");

                /*Si urgence, on déclenche le menu d'actions*/
                if (grele || rongeur)
                    Urgence.GererUrgence();

                /*Pour chaque plante du potager, on vérifie son état*/
                for (int i = potager.Count - 1; i >= 0; i--)
                {
                    Plante plante = potager[i];
                    int conditionsOK = plante.NombreConditionsRespectees(terrain, temperature, luminosite, humidite);
                    double proportion = conditionsOK / 5.0;

                    if (proportion < 0.5 || plante.EsperanceDeVie <= 0)
                    {
                        Console.WriteLine($"🪴 {plante.Nom} est morte (conditions non remplies ou vieillesse).\n");
                        potager.RemoveAt(i);
                        continue;
                    }

                    if (rand.NextDouble() < plante.ProbabiliteContamination)
                    {
                        Console.WriteLine($"🪴 {plante.Nom} est tombée malade ({plante.Maladies[0]}) et a été détruite.\nLa plante est malade.");
                        potager.RemoveAt(i);
                        continue;
                    }

                    /*Affichage de l'état de la plante*/
                    Console.WriteLine($" {plante.Nom} |🧑‍⚕️ Santé: {plante.Sante}/100 |💗 Vie: {plante.EsperanceDeVie} |💹 Croissance: {plante.VitesseCroissance * proportion:0.0} | Rendement: {(int)(plante.Rendement * proportion)}");
                    plante.EffectuerAction(pluie);
                    plante.EsperanceDeVie--;
                }

                AfficherInventaire();

                /*Si le terrain a de la place, on propose de planter une nouvelle graine*/
                if (potager.Count < terrain.Capacite)
                {
                    Console.WriteLine("⛏️ Planter une nouvelle graine ? (oui/non)");
                    if (Console.ReadLine()!.ToLower() == "oui")
                    {
                        for (int i = 0; i < plantesDisponibles.Count; i++)
                        {
                            Plante p = plantesDisponibles[i];
                            Console.WriteLine($"{i + 1}. {p.Nom} — Terrain: {p.TerrainPrefere}, Espacement: {p.EspacementMin},⏲️ Temp: {p.TempMin}-{p.TempMax}°C, Humidité min: {p.HumMin}%,☀️ Lumi min: {p.LumiMin}%,💗 Vie: {p.EsperanceDeVie} semaines, Maladie: {string.Join(", ", p.Maladies)} ({p.ProbabiliteContamination * 100}%)");
                        }

                        int choix = int.Parse(Console.ReadLine()!);
                        Plante planteChoisie = plantesDisponibles[choix - 1];
                        /*On crée une vraie nouvelle plante à partir de celle choisie (copie indépendante)*/
                        potager.Add(new Plante(
                            planteChoisie.Nom,
                            planteChoisie.TerrainPrefere,
                            planteChoisie.EspacementMin,
                            planteChoisie.TempMin,
                            planteChoisie.TempMax,
                            planteChoisie.HumMin,
                            planteChoisie.LumiMin,
                            planteChoisie.EsperanceDeVie,
                            planteChoisie.Nature,
                            planteChoisie.Comestible,
                            new List<string>(planteChoisie.SaisonsSemis),
                            planteChoisie.VitesseCroissance,
                            new List<string>(planteChoisie.Maladies),
                            planteChoisie.ProbabiliteContamination,
                            planteChoisie.Rendement
                        ));
                    }
                }
                else
                {
                    Console.WriteLine("😦 Terrain plein ! Changement de terrain...\n");
                    terrain = CreerTerrainAleatoire();
                    potager.Clear();
                }

                /*Fin de semaine : on passe à la suivante*/
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("🙂 Appuyer sur Entrée pour passer à la semaine suivante...");
                Console.ReadLine();
                semaine++;
                semainesRestantesDansSaison--;

                /*Si plus aucune plante n'est vivante, on termine le jeu*/
                if (potager.Count == 0)
                {
                    Console.WriteLine("🥹 Toutes les plantes sont mortes. Fin du jeu.");
                    break;
                }
            }
        }
    }
}
