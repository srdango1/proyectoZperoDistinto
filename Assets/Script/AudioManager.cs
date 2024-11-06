
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header ("-------AudioSource--------")]
   [SerializeField] AudioSource fuenteMusica;
   [SerializeField] AudioSource fuenteEfectos;
   [SerializeField] AudioSource fuentePasos;
    

    
    [Header ("-------AudioClip--------")]
    // Aqui van los audioClip es decir un espacio para poner el audio y poder seleccionarlo por nombre
   public AudioClip fondo;
   [SerializeField]public AudioClip[] caminar;
   public AudioClip playerAtaque;
   public AudioClip coleccionable;
   [SerializeField] private AudioClip[] sonidosDaño;
   [SerializeField] private AudioClip[] sonidosMuertes;
   public AudioClip vida;
   public AudioClip salto;
   public AudioClip aterrizar;
   public AudioClip golpePlayer;
   public AudioClip muertePlayer;
   public AudioClip caidaPlayer;
   public AudioClip respawn;


   
   private void Start(){
    //Esto es basicamente que el fuenteMusica tome el clip de fondo y se inicie la musica en cuanto 
    //se inicie el juego
    fuenteMusica.clip=fondo;
    fuenteMusica.Play();
   }
    // Play efectos es el que se llama y utiliza en cada cosa que haga ruidos del juego
    //por ejemplo: recoger algo,golpear, etc.
    public void PlayEfectos (AudioClip clip){
        fuenteEfectos.PlayOneShot(clip);
    }

    public void Pasos()
    {
        if (!fuentePasos.isPlaying){
        fuentePasos.clip = caminar[0];
        fuentePasos.loop = true;
        fuentePasos.Play();
    }
    }

    public void paraPasos(){
        if (fuentePasos.isPlaying)
        {
            fuentePasos.Stop();
        }
    }
    
    public void DañoEnemigo(string nom)
    {
        int index = indexEnemigo(nom);
        if (index>=0 && index< sonidosDaño.Length){
            fuenteEfectos.PlayOneShot(sonidosDaño[index]);
        }
    }

     public void MuerteEnemigo(string nom)
    {
        int index = indexEnemigo(nom);
        if (index>=0 && index< sonidosMuertes.Length){
            fuenteEfectos.PlayOneShot(sonidosMuertes[index]);
        }
    }
    private int indexEnemigo(string nom)
    {
        switch (nom.ToLower())
        {
            case "esqueleto":
                return 0;
            case "golem":
                return 1;
            case "slime":
                return 2;
            case "escorpion":
                return 3;
            case "boss":
                return 4;
            default: 
                return -1;
            
        }
    }

}
