using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor.SearchService;
#endif

public class GeneradorDeEscenario : MonoBehaviour
{
    public GameObject tomatoPlant;
    public GameObject tomatoPlant2;
    public GameObject[] optionTomatoes;
    public GameObject[] optionPlants;
    public GameObject leaf;
    public GameObject earth;
    public GameObject structure;
    public GameObject lamp; 
    public GameObject flowerpot;
    public GameObject cubeEarth;
    public GameObject metalSuport;
    public Material Hoja1;
    public Material Hoja2;
    MeshRenderer meshRendererHoja;
    public int rowL= 2; //n_Rows
    public int n_Rows= 2; //numberRows
    public int dPlant= 1; //dPlant
    public int dRow= 1; //dRow

    public float lampIntensity=1;

    public int GlassHouseperline= 1;

    public int linesofGlassHouse= 1;

    
    void Start()
    {

       //This is the tomato land
        //GenerarLand();
       
       //This is for the Glass House
       autoGlassHouse(n_Rows, rowL);
    }
    void GenerateGlassHouse(float g, float h)
    {
      ///////////////////ROW_LENGTH_CALIBRATION///////////////////////////////////   
        if(n_Rows<=19 ){
            if(dRow==1){
                Debug.LogWarning("Continue ");              
                gener(rowL, dPlant, g, h);
            }
            
            if(dRow==2){

                if(n_Rows>10){
                    Debug.LogWarning("No space for more plants ");
                }
                if(n_Rows<=10){
                    Debug.LogWarning("Continue ");
                    gener(rowL, dPlant, g, h);

                }
            }

            if(dRow==3){
                if(n_Rows>7){
                    Debug.LogWarning("No space for more plants ");
                }

                if(n_Rows<=7){
                    Debug.LogWarning("Continue ");
                    gener(rowL, dPlant, g, h);
                }
            }

            if(dRow==4){
                if(n_Rows>5){
                    Debug.LogWarning("No space for more plants ");  
                }

                if(n_Rows<=5){
                    Debug.LogWarning("Continue ");
                    gener(rowL, dPlant, g, h);

                }
                
            }
            if(dRow==5 || dRow==6){
                if(n_Rows>4){
                    Debug.LogWarning("No space for more plants ");
                    
                }

                if(n_Rows<=4){
                    Debug.LogWarning("Continue ");
                    gener(rowL, dPlant, g, h);
                }

            }
   
            if(dRow==7 || dRow==8){
                if(n_Rows>3){
                    Debug.LogWarning("No space for more plants ");
                    
                }

                if(n_Rows<=3){
                    Debug.LogWarning("Continue ");
                    gener(rowL, dPlant, g, h);
                }

            }

            if(dRow==9 || dRow==10 || dRow==11 || dRow==12|| dRow==13|| dRow==14|| dRow==15|| dRow==16|| dRow==17||dRow==18 ){
                if(n_Rows>2){
                    Debug.LogWarning("No space for more plants ");
                }

                if(n_Rows<=2){
                    Debug.LogWarning("Continue ");
                    gener(rowL, dPlant, g, h);
  
                }
            }
        }

        if(n_Rows>=19){
            Debug.LogWarning("No space for more plants ");
        }
    
    }

    void gener(int rowL, int dPlant, float g, float h)
    {
            //_____________________DISTANCE_ROWS_CALIBRATION______________________________
        if(rowL<9 ){

            if(dPlant==1){
                if(rowL>9){
                    Debug.LogWarning("No space for more plants ");
                }
                
                if(rowL<=9){    
                    for (int x= 0; x<n_Rows; x++){
                    
                        Vector3 posMetal= new Vector3(x*dRow-9+25.3f*(g), -0.344f, 0*dPlant-16.3f+(11.245f * h)-.5f);
                        GameObject metal = Instantiate(metalSuport, posMetal, Quaternion.Euler(0,90,0));
                           
                        for (int z= 0; z< rowL; z++){

                            /////////////////////////////////////////////////////////////////////
                            Vector3 posPlants= new Vector3(x*dRow-9+25.3f*(g), 0.55f, z*dPlant-16.3f+(11.245f * h));
                            Vector3 posFlowerPot= new Vector3(x*dRow-9+25.3f*(g), 0.51f, z*dPlant-16.3f+(11.245f * h));
                            
                            GameObject nuevoTerreno = Instantiate(tomatoPlant, posPlants, Quaternion.identity);
                            GameObject flowerpot1= Instantiate(flowerpot, posFlowerPot, Quaternion.Euler(0,90,0));
                            
                            Vector3 posBlock= new Vector3(x*dRow-9+25.3f*(g), 0.4f, z*dPlant-16.3f+(11.245f * h)); 
                            GameObject blockE = Instantiate(cubeEarth, posBlock, Quaternion.identity); 
                             
                            ////////////////////////////////////////////////////////////////////////
                            if(x%2==0){
                                int randomIndex=  UnityEngine.Random.Range(0,4);
                                TomatoRandom(x,z, 0.55f, randomIndex, g, h);
                            }
                            if(x%2!=0){
                                int randomIndex=  UnityEngine.Random.Range(4,8);
                                TomatoRandom(x,z, 0.55f, randomIndex, g, h);
                            }
                            ///////////////////////////////////////////////////////////////////////
                            
                            Vector3 posLamp= new Vector3(x*dRow-9+25.3f*(g), 2.4f, z*dPlant-16.3f+(11.245f * h)); 
                            GameObject lamp1 = Instantiate(lamp, posLamp, Quaternion.Euler(180,90,0));
                            Light lightComponent = lamp1.AddComponent<Light>();
                            Color colorF200FF;
                            UnityEngine.ColorUtility.TryParseHtmlString("#F200FF", out colorF200FF);
                            lightComponent.type = LightType.Point;
                            lightComponent.color = colorF200FF;

                            lightComponent.intensity = lampIntensity;
                            
                            lightComponent.range = 10f;
                        }
                    }



                }
            }

            if(dPlant==8 || dPlant==7|| dPlant==6|| dPlant==5){
                if(rowL>2){
                    Debug.LogWarning("No space for more plants ");
                }

                if(rowL<=2){
                    
                    for (int x= 0; x<n_Rows; x++){
                        
                        Vector3 posMetal= new Vector3(x*dRow-9+25.3f*(g), -0.344f, 0*dPlant-16.3f+(11.245f * h)-.5f);
                        GameObject metal = Instantiate(metalSuport, posMetal, Quaternion.Euler(0,90,0));
                        for (int z= 0; z< rowL; z++){
                            ////////////////////////////////////////////////////////////////////////////
                            Vector3 posPlants= new Vector3(x*dRow-9+25.3f*(g), 0.55f, z*dPlant-16.3f+(11.245f * h));
                            Vector3 posFlowerPot= new Vector3(x*dRow-9+25.3f*(g), 0.51f, z*dPlant-16.3f+(11.245f * h));
                            GameObject nuevoTerreno = Instantiate(tomatoPlant, posPlants, Quaternion.identity);
                            GameObject flowerpot1= Instantiate(flowerpot, posFlowerPot, Quaternion.Euler(0,90,0));
                            
                            Vector3 posBlock= new Vector3(x*dRow-9+25.3f*(g), 0.4f, z*dPlant-16.3f+(11.245f * h)); 
                            GameObject blockE = Instantiate(cubeEarth, posBlock, Quaternion.identity); 
                            

                            //////////////////////////////////////////////////////////////////////
                            if(x%2==0){
                                int randomIndex=  UnityEngine.Random.Range(0,4);
                                TomatoRandom(x,z, 0.55f, randomIndex, g, h);
                            }
                            if(x%2!=0){
                                int randomIndex=  UnityEngine.Random.Range(4,8);
                                TomatoRandom(x,z, 0.55f, randomIndex, g, h);
                            }
                            ////////////////////////////////////////////////////////////////////////////
                            Vector3 posLamp= new Vector3(x*dRow-9+25.3f*(g), 2.4f, z*dPlant-16.3f+(11.245f * h)); 
                            GameObject lamp1 = Instantiate(lamp, posLamp, Quaternion.Euler(180,90,0));
                            Light lightComponent = lamp1.AddComponent<Light>();
                            Color colorF200FF;
                            UnityEngine.ColorUtility.TryParseHtmlString("#F200FF", out colorF200FF);
                            lightComponent.type = LightType.Point;
                            lightComponent.color = colorF200FF;
                            lightComponent.intensity = lampIntensity;
                            lightComponent.range = 10f;
                        }
                    }


                }

            }

            if(dPlant==4 || dPlant==3){
                if(rowL>3){
                    Debug.LogWarning("No space for more plants ");
                }
                if(rowL<=3){
                    
                    for (int x= 0; x<n_Rows; x++){

                        Vector3 posMetal= new Vector3(x*dRow-9+25.3f*(g), -0.344f, 0*dPlant-16.3f+(11.245f * h)-.5f);
                        GameObject metal = Instantiate(metalSuport, posMetal, Quaternion.Euler(0,90,0));
                        for (int z= 0; z< rowL; z++){
                            /////////////////////////////////////////////////////
                            Vector3 posPlants= new Vector3(x*dRow-9+25.3f*(g), 0.55f, z*dPlant-16.3f+(11.245f * h));
                            Vector3 posFlowerPot= new Vector3(x*dRow-9+25.3f*(g), 0.51f, z*dPlant-16.3f+(11.245f * h));
                            GameObject nuevoTerreno = Instantiate(tomatoPlant, posPlants, Quaternion.identity);
                            GameObject flowerpot1= Instantiate(flowerpot, posFlowerPot, Quaternion.Euler(0,90,0));
                            
                            Vector3 posBlock= new Vector3(x*dRow-9+25.3f*(g), 0.4f, z*dPlant-16.3f+(11.245f * h)); 
                            GameObject blockE = Instantiate(cubeEarth, posBlock, Quaternion.identity); 
                        
                            //////////////////////////////////////////////////////
                            if(x%2==0){
                                int randomIndex=  UnityEngine.Random.Range(0,4);
                                TomatoRandom(x,z, 0.55f, randomIndex, g, h);
                            }
                            if(x%2!=0){
                                int randomIndex=  UnityEngine.Random.Range(4,8);
                                TomatoRandom(x,z, 0.55f, randomIndex, g, h);
                            }
                            /////////////////////////////////////////////////////
                            Vector3 posLamp= new Vector3(x*dRow-9+25.3f*(g), 2.4f, z*dPlant-16.3f+(11.245f * h)); 
                            GameObject lamp1 = Instantiate(lamp, posLamp, Quaternion.Euler(180,90,0));
                            Light lightComponent = lamp1.AddComponent<Light>();
                            Color colorF200FF;
                            UnityEngine.ColorUtility.TryParseHtmlString("#F200FF", out colorF200FF);
                            lightComponent.type = LightType.Point;
                            lightComponent.color = colorF200FF;
                            lightComponent.intensity = lampIntensity;
                            lightComponent.range = 10f;
                        }
                    }

                }
                
            }

            if(dPlant==2){   
                if(rowL>5){
                    Debug.LogWarning("No space for more plants ");

                }

                if(rowL<=5){
                    
                    for (int x= 0; x<n_Rows; x++){

                        Vector3 posMetal= new Vector3(x*dRow-9+25.3f*(g), -0.344f, 0*dPlant-16.3f+(11.245f * h)-.5f);
                        GameObject metal = Instantiate(metalSuport, posMetal, Quaternion.Euler(0,90,0));
                        for (int z= 0; z< rowL; z++){
                            //////////////////////////////////////////////////////////////////////
                            Vector3 posPlants= new Vector3(x*dRow-9+25.3f*(g), 0.55f, z*dPlant-16.3f+(11.245f * h));
                            Vector3 posFlowerPot= new Vector3(x*dRow-9+25.3f*(g), 0.51f, z*dPlant-16.3f+(11.245f * h));
                            GameObject nuevoTerreno = Instantiate(tomatoPlant, posPlants, Quaternion.identity);
                            GameObject flowerpot1= Instantiate(flowerpot, posFlowerPot, Quaternion.Euler(0,90,0));
                            
                            Vector3 posBlock= new Vector3(x*dRow-9+25.3f*(g), 0.4f, z*dPlant-16.3f+(11.245f * h)); 
                            GameObject blockE = Instantiate(cubeEarth, posBlock, Quaternion.identity);

                            if(x%2==0){
                                int randomIndex=  UnityEngine.Random.Range(0,4);
                                TomatoRandom(x,z, 0.55f, randomIndex, g, h);
                            }
                            if(x%2!=0){
                                int randomIndex=  UnityEngine.Random.Range(4,8);
                                TomatoRandom(x,z, 0.55f, randomIndex, g, h);
                            }
                            /////////////////////////////////////////////////////////////////////////
                            Vector3 posLamp= new Vector3(x*dRow-9+25.3f*(g), 2.4f, z*dPlant-16.3f+(11.245f * h)); 
                            GameObject lamp1 = Instantiate(lamp, posLamp, Quaternion.Euler(180,90,0));
                            Light lightComponent = lamp1.AddComponent<Light>();
                            Color colorF200FF;
                            UnityEngine.ColorUtility.TryParseHtmlString("#F200FF", out colorF200FF);
                            lightComponent.type = LightType.Point;
                            lightComponent.color = colorF200FF;
                            lightComponent.intensity = lampIntensity;
                            lightComponent.range = 10f; 
                        }
                    }

                }
             
            }


            if(dPlant<=0  || dPlant>=9){
                 Debug.LogWarning("No space for more plants ");
            }

        }
        
        if (rowL>=9){
            Debug.LogWarning("No space for more plants ");
        }
        


    }

    void GenerarLand()
    {
        earth.transform.localScale = new Vector3(dRow*.1f, 1f, dPlant*.1f);

        for (int x = 0; x < n_Rows; x++)
        {   
            for (int z = 0; z < rowL; z++)
            {
                Vector3 pos = new Vector3(x*dRow, 0, z*dPlant);

                int tomatoNumber= UnityEngine.Random.Range(1,2);
                int leafNumber= UnityEngine.Random.Range(3,6);    
                int randomSpecial= UnityEngine.Random.Range(0,1);
                int randomSpecial1= UnityEngine.Random.Range(0,1);
                GameObject randomPlant = UnityEngine.Random.Range(0, 2) == 0 ? tomatoPlant : tomatoPlant2;
                //GameObject randomTomatoe = UnityEngine.Random.Range(0, 2) == 0 ? tomato1_Green : tomato2_Green;
                int randomIndex=  UnityEngine.Random.Range(0,8);
                GameObject randomTomatoe = optionTomatoes[randomIndex];
                
                //int randomIndex= UnityEngine.Random.Range(0, 8);
                //GameObject randomTomatoe = objects[randomIndex];


                // == 0 ? tomato1_Red: tomato2_Red;


                //tomato1_Red, tomato1_Green, tomato1_OrangeStrong, tomato1_OrangeWeak
                //tomato2_Red, tomato2_Green, tomato2_OrangeStrong, tomato2_OrangeWeak

                GameObject nuevoTerreno = Instantiate(randomPlant, pos, Quaternion.identity);
                GameObject planeEarth = Instantiate(earth, pos, Quaternion.identity);
                
                if (randomPlant== tomatoPlant){
                // 1
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 1.2f);
                    Vector3 posLeaf= new Vector3(x*dRow+0.0156f, rYL, z*dPlant-0.0034f);
                    GameObject leaf1 = Instantiate(leaf, posLeaf, Quaternion.identity);
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.8f, 0f);
                    Vector3 posTomato = new Vector3(x*dRow-0.103f, randomY, z*dPlant+0.025f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.identity);
                } 

                //2 
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 1.2f);
                    Vector3 posLeaf= new Vector3(x*dRow+0.0258f, rYL, z*dPlant-0.0117f);
                    GameObject leaf1= Instantiate(leaf, posLeaf,Quaternion.Euler(0,180,0));
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.8f, 0f);
                    Vector3 posTomato = new Vector3(x*dRow+0.091f, randomY, z*dPlant-0.03f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,180,0));
                }

                //3
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 1.2f);
                    Vector3 posLeaf= new Vector3(x*dRow+0.0163f,rYL,z*dPlant-0.0136f);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,-90,0));
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.8f, 0f);
                    Vector3 posTomato = new Vector3(x*dRow-0.03f, randomY, z*dPlant-0.1f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,-90,0));
                }
               
                //4
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 1.2f);
                    Vector3 posLeaf= new Vector3(x*dRow+0.014f,rYL,z*dPlant-0.007f);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,90,0));
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.8f, 0f);
                    Vector3 posTomato = new Vector3(x*dRow+0.027f, randomY, z*dPlant+0.1f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,90,0));
                    }
            
                }

                if(randomPlant== tomatoPlant2){
                
                ///////1////////
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 0.848f);
                    Vector3 posLeaf= new Vector3(x*dRow+0.0207f, rYL, z*dPlant-0.015f);
                    GameObject leaf1 = Instantiate(leaf, posLeaf, Quaternion.identity);
                    
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.755f, -0.218f);
                    Vector3 posTomato = new Vector3(x*dRow-0.064f, randomY, z*dPlant+0.0112f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.identity);
                }


                /////////2/////////////////////////

                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 0.848f);
                    Vector3 posLeaf= new Vector3(x*dRow+0.0117f, rYL, z*dPlant-0.010f);
                    GameObject leaf1= Instantiate(leaf, posLeaf,Quaternion.Euler(0,180,0));
                    
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.755f, -0.218f);
                    Vector3 posTomato = new Vector3(x*dRow+0.109f, randomY, z*dPlant-0.041f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,180,0));
                }

                ///////3/////////////////////////////
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 0.848f);
                    Vector3 posLeaf= new Vector3(x*dRow+0.009f,rYL,z*dPlant-0.005f);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,-90,0));
                    
                }
                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.755f, -0.218f);
                    Vector3 posTomato = new Vector3(x*dRow-0.008f, randomY, z*dPlant-0.081f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,-90,0));
                }

                /////////////////4//////////////////////
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 0.848f);
                    Vector3 posLeaf= new Vector3(x*dRow+0.011f,rYL,z*dPlant-0.001f);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,90,0));  
                                    
                    }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.755f, -0.218f);
                    Vector3 posTomato = new Vector3(x*dRow+0.0689f, randomY, z*dPlant+0.0533f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,90,0));
                    
                }

                //Extra tomato
            
                for(int t=0; t<randomSpecial;t++){
                    float randomY = UnityEngine.Random.Range(-0.210f, -0.232f);
                    Vector3 posTomato = new Vector3(x*dRow+0.3164f, randomY, z*dPlant+0.083f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,90,0));
                }

                for(int t=0; t<randomSpecial1;t++){
                    float randomY = UnityEngine.Random.Range(-0.126f, -0.130f);
                    Vector3 posTomato = new Vector3(x*dRow+0.209f, randomY, z*dPlant-0.097f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,-90,0));
                }
        
                }
                //Leaf modifications
                for(int l=0; l<0; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 0.848f);
                    Vector3 posLeaf= new Vector3(x*dRow,rYL,z*dPlant);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,90,0));  
                                    
                    }

                    
            }
        }
    }

    void TomatoRandom(float x, float z, float y, int randomIndex, float g, float h){
                Vector3 posPlants= new Vector3(x*dRow-9+25.3f*(g), 0.55f, z*dPlant-16.3f);

                int tomatoNumber= UnityEngine.Random.Range(1,2);
                int leafNumber= UnityEngine.Random.Range(3,5);    
                int randomSpecial= UnityEngine.Random.Range(0,1);
                int randomSpecial1= UnityEngine.Random.Range(0,1);
                GameObject randomPlant = UnityEngine.Random.Range(0, 2) == 0 ? tomatoPlant : tomatoPlant2;
                GameObject randomTomatoe = optionTomatoes[randomIndex];

                
                if (randomPlant== tomatoPlant){
                // 1
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+1.2f);
                    Vector3 posLeaf= new Vector3(x*dRow-9+25.3f*(g), rYL, z*dPlant-16.3f+11.245f * h);
                    GameObject leaf1 = Instantiate(leaf, posLeaf, Quaternion.identity);
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.8f, y+0f);
                    Vector3 posTomato = new Vector3(x*dRow-9+25.3f*(g), randomY, z*dPlant-16.3f+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.identity);
                } 

                //2 
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+1.2f);
                    Vector3 posLeaf= new Vector3(x*dRow-9+25.3f*(g), rYL, z*dPlant-16.3f+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf,Quaternion.Euler(0,180,0));
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.8f, y+0f);
                    Vector3 posTomato = new Vector3(x*dRow-9+25.3f*(g), randomY, z*dPlant-16.3f+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,180,0));
                }

                //3
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+1.2f);
                    Vector3 posLeaf= new Vector3(x*dRow-9+25.3f*(g),rYL,z*dPlant-16.3f+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,-90,0));
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.8f, y+0f);
                    Vector3 posTomato = new Vector3(x*dRow-9+25.3f*(g), randomY, z*dPlant-16.3f+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,-90,0));
                }
               
                //4
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+1.2f);
                    Vector3 posLeaf= new Vector3(x*dRow-9+25.3f*(g),rYL,z*dPlant-16.3f+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,90,0));
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.8f, y+0f);
                    Vector3 posTomato = new Vector3(x*dRow-9+25.3f*(g), randomY, z*dPlant-16.3f+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,90,0));
                    }
            
                }

                if(randomPlant== tomatoPlant2){
                
                ///////1////////
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+0.848f);
                    Vector3 posLeaf= new Vector3(x*dRow-9+25.3f*(g), rYL, z*dPlant-16.3f+11.245f * h);
                    GameObject leaf1 = Instantiate(leaf, posLeaf, Quaternion.identity);
                    
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.755f, y+-0.218f);
                    Vector3 posTomato = new Vector3(x*dRow-9+25.3f*(g), randomY, z*dPlant-16.3f+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.identity);
                }


                /////////2/////////////////////////

                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+0.848f);
                    Vector3 posLeaf= new Vector3(x*dRow-9+25.3f*(g), rYL, z*dPlant-16.3f+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf,Quaternion.Euler(0,180,0));
                    
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.755f, y+-0.218f);
                    Vector3 posTomato = new Vector3(x*dRow-9+25.3f*(g), randomY, z*dPlant-16.3f+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,180,0));
                }

                ///////3/////////////////////////////
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+0.848f);
                    Vector3 posLeaf= new Vector3(x*dRow-9+25.3f*(g),rYL,z*dPlant-16.3f+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,-90,0));
                    
                }
                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.755f, y+-0.218f);
                    Vector3 posTomato = new Vector3(x*dRow-9+25.3f*(g), randomY, z*dPlant-16.3f+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,-90,0));
                }

                /////////////////4//////////////////////
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+0.848f);
                    Vector3 posLeaf= new Vector3(x*dRow-9+25.3f*(g),rYL,z*dPlant-16.3f+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,90,0));  
                                    
                    }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.755f, y+-0.218f);
                    Vector3 posTomato = new Vector3(x*dRow-9+25.3f*(g), randomY, z*dPlant-16.3f+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,90,0));
                    
                }

                //Extra tomato
            
                for(int t=0; t<randomSpecial;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.210f, y+-0.232f);
                    Vector3 posTomato = new Vector3(x*dRow-9+25.3f*(g), randomY, z*dPlant-16.3f+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,90,0));
                }

                for(int t=0; t<randomSpecial1;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.126f, y+-0.130f);
                    Vector3 posTomato = new Vector3(x*dRow-9+25.3f*(g), randomY, z*dPlant-16.3f+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,-90,0));
                }
        
                }
                //Leaf modifications
                for(int l=0; l<0; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+0.848f);
                    Vector3 posLeaf= new Vector3(x*dRow-9+25.3f*(g),rYL,z*dPlant-16.3f+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,90,0));               
                    }
    }



    void LandTomatoes(){

        earth.transform.localScale = new Vector3(dRow*.1f, 1f, dPlant*.1f);

        for (int x = 0; x < n_Rows; x++)
        {   
            for (int z = 0; z < rowL; z++)
            {
                Vector3 pos = new Vector3(x*dRow, 0, z*dPlant);
                GameObject nuevoTerreno = Instantiate(tomatoPlant, pos, Quaternion.identity);
                GameObject planeEarth = Instantiate(earth, pos, Quaternion.identity);
                
            }
        }

    }

    void oneGlassHouse(){
        for (int g=0; g<GlassHouseperline; g++){
            for (int h=0; h<linesofGlassHouse; h++){
                Vector3 posStructure= new Vector3(10*g*2.5f, 0, 17.3f*h*0.65f);
                GameObject structureGlassHouse= Instantiate(structure, posStructure, Quaternion. identity);
            }
        }
    }
    void autoGlassHouse(int n_Rows, int rowL) {

    for (int g = 0; g < GlassHouseperline; g++) {
        for (int h = 0; h < linesofGlassHouse; h++) {
            Vector3 posStructure = new Vector3(25.3f*g, 0, 11.245f * h );
            GameObject structureGlassHouse = Instantiate(structure, posStructure, Quaternion.Euler(0,180,0));
            GenerateGlassHouse(g, h); 

            }
        }
    }
          
}



