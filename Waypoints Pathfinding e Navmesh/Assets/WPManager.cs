using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Serializa a classe
public struct Link
{
    public enum direction { UNI, BI } //Cria um enumerador Uni para uma direção apenas e Bi para ida e volta
    public GameObject node1; //Cria o node1
    public GameObject node2; //Cria o node2
    public direction dir; //Cria a varavel de direção
}

public class WPManager : MonoBehaviour
{
    public GameObject[] waypoints; //Pega os waypoints colocados no mapa;
    public Link[] links; //Armazena os waypoints
    public Graph graph = new Graph(); //Instancia o Graph

    void Start()
    {
        if (waypoints.Length > 0) //Verificando se o tamanho do waypoint é menor que 0
        {
            foreach (GameObject wp in waypoints) //adiciona waypoints
            {
                graph.AddNode(wp); //Adiciona um node no Graph
            }
            foreach (Link l in links) //Adiciona links
            {
                graph.AddEdge(l.node1, l.node2);
                if (l.dir == Link.direction.BI) //verificando se é UNI ou BI
                    graph.AddEdge(l.node2, l.node1);
            }
        }
    }

    void Update()
    {
        graph.debugDraw(); //Mostra no console
    }
}

