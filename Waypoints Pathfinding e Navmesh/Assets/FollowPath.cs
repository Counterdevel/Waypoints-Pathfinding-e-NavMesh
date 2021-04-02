using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    Transform goal; //Passa a posição para o tank seguir
    float speed = 5.0f; //Velocidade de movimento
    float accuracy = 1.0f; //Pega a precisão da distancia entre os waypoints e o tank
    float rotSpeed = 2.0f; //Velocidade da rotação
    public GameObject wpManager; //Pega o gameobject que contêm o script WPManager
    GameObject[] wps; //Pega os waypoints colocados no mapa;
    GameObject currentNode; //Mostra qual é o node atual
    int currentWP = 0; //Mostra qual é o waypoint atual
    Graph g; //Instancia do Graph

    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;         //Pegando os components das variaveis instanciadas e define o node atual
        currentNode = wps[0];
    }
    public void GoToHeli() //Método que passa a localização para chegar no heliporto no mapa
    {
        g.AStar(currentNode, wps[9]); //Faz com que o tank percorra o caminho até o heliporto
        currentWP = 0;  //reseta o waypoint;
    }
    public void GoToRuin() //Método que passa a localização para chegar nas ruínas no mapa
    {
        g.AStar(currentNode, wps[6]); //Faz com que o tank percorra o caminho até as ruínas
        currentWP = 0; //reseta o waypoint;
    }

    void LateUpdate()
    {
        if (g.getPathLength() == 0 || currentWP == g.getPathLength()) //Verificando se o tamanho do Graph [e igual a 0 ou se o waypoint atual é igual ao tamanho do Graph
            return;
        
        currentNode = g.getPathPoint(currentWP); //Pega o node mais próximo do tank
        
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, //se o tank estiver a uma distância menor que o tamanho da accuracy o tank passa para o próximo waypoint
        transform.position) < accuracy)
        {
            currentWP++; //Acresenta 1 para o index dos waypoints
        }

        if (currentWP < g.getPathLength()) //verificando se o tamanho do waypoint atual é menor que o tamanho do Graph
        {
            goal = g.getPathPoint(currentWP).transform; //setando a posição para o tank seguir
            Vector3 lookAtGoal = new Vector3(goal.position.x, //setando para qual direção o tank vai virar
            this.transform.position.y,
            goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position; //Passa a direção e a rotação do tank para suavizar o movimento
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotSpeed);
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime); //Adiciona o movimento para o tank
    }

}
