using System;

// Archer > Dragon
// Dragon > Soldat
// Soldat > Golem 
// Golem > Archer
// Sorcier > Golem, dragon 
// Boss > Sorcier, soldat et archer (Boss chance de 25% de spawn)

// Archer : Precision : 75% | Degat : 25pts | Vie : 100
// Soldat : Precision : 50% | Degat : 50pts | Vie : 120
// Sorcier : Precision : 25% | Degat : 100pts | Vie : 90

// Golem : Precision : 25% | Degat : 50pts | Vie : 110
// Dragon : Precision : 50% | Degat : 75pts | Vie : 95
// Boss : Precision : 100% | Degat : 110 | Vie : 200

// Support apporte soins(+ 50pts de vie)
// boost apporte : Degat *2 



// CLASSE PERSONNAGES

using System;

interface IAttaquable
{
    void Attaquer(Personnage cible);
}

class Personnage
{
    public int Vie { get; protected set; }
    public int Degat { get; }
    public string Nom { get; }

    public Personnage(string nom, int vie, int degat)
    {
        Nom = nom;
        Vie = vie;
        Degat = degat;
    }

    public void AfficherInfo()
    {
        Console.WriteLine($"{Nom} - Vie: {Vie}, Dégâts: {Degat}");
    }

    public void Attaquer(Personnage cible)
    {
        if(Nom == "Soldat" && cible.Nom == "Dragon") // Le dragon à un avantage sur le soldat
        {
            cible.Vie -= Degat/2;
            Console.WriteLine($"{Nom} attaque {cible.Nom} ! {cible.Nom} perd seulement {Degat/2} points de vie du à une esquive du dragon dans les air ! {cible.Nom} a maintenant {cible.Vie} points de vie.");
        }
        else {
            cible.Vie -= Degat;
            Console.WriteLine($"{Nom} attaque {cible.Nom} ! {cible.Nom} perd {Degat} points de vie. {cible.Nom} a maintenant {cible.Vie} points de vie.");
        }
        
    }
}

class Joueur : Personnage, IAttaquable
{
    public Joueur(string nom, int vie, int degat) : base(nom, vie, degat) { }

    public void JoueurAttack(Personnage monstre)
    {
        //Console.WriteLine($"{Nom} attaque {monstre.Nom} !");
        Attaquer(monstre);
    }
}

class Monstre : Personnage, IAttaquable
{
    public Monstre(string nom, int vie, int degat) : base(nom, vie, degat) { }

    public void MonstreAttack(Personnage joueur)
    {
        //Console.WriteLine($"{Nom} attaque le joueur !");
        Attaquer(joueur);
    }
}

class Program
{
    static Random random = new Random();

    static void Main()
    {
        // CHOIX DU JOUEUR
        // Demander à l'utilisateur de choisir une classe
        string choixClasse = ChoisirClasse();

        // Instancier le joueur avec la classe choisie
        Joueur joueur = CreerJoueur(choixClasse);

        // CHOIX D'UN MONSTRE AU HASARD
        Monstre monstre = CreerMonstreAleatoire();

        // LANCEMENT DU COMBAT
        while (joueur.Vie > 0 && monstre.Vie > 0)
        {
            // Joueur attaque le monstre
            joueur.JoueurAttack(monstre);

            // Vérifier si le monstre est encore en vie après l'attaque du joueur
            if (monstre.Vie > 0)
            {
                // Monstre attaque le joueur
                monstre.MonstreAttack(joueur);
            }
        }

        // Afficher le résultat
        if (joueur.Vie <= 0)
        {
            Console.WriteLine("Le joueur est mort. Game over !");
        }
        else
        {
            Console.WriteLine("Le monstre est mort. Félicitations !");
        }
    }

    // CHOIX DE LA CLASSE DE LA PART DU JOUEUR
    static string ChoisirClasse()
    {
        Console.WriteLine("Choisissez une classe : Archer | Sorcier | Soldat");
        return Console.ReadLine().ToLower();
    }

    // Générer aléatoirement un monstre
    static Monstre CreerMonstreAleatoire()
    {
        int typeMonstre = random.Next(1, 4);

        switch (typeMonstre)
        {
            case 1:
                return new Monstre("Golem", 110, 50);
            case 2:
                return new Monstre("Dragon", 95, 75);
            case 3:
                return new Monstre("Boss", 200, 110);
            default:
                Console.WriteLine("Erreur lors de la génération du monstre.");
                return null;
        }
    }

    // Instancier le joueur avec la classe choisie
    static Joueur CreerJoueur(string choixClasse)
    {
        switch (choixClasse)
        {
            case "archer":
                return new Joueur("Archer", 100, 25);
            case "sorcier":
                return new Joueur("Sorcier", 90, 100);
            case "soldat":
                return new Joueur("Soldat", 120, 50);
            default:
                Console.WriteLine("Classe non reconnue.");
                return null;
        }
    }
}
