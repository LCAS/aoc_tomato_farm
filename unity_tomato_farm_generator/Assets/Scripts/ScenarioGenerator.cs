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

public class ScenarioGenerator : MonoBehaviour
{
    
    public GameObject branch;
    public GameObject branch2;
    public GameObject[] optionTomatoes;
    public GameObject leaf;
    public GameObject earth;
    public GameObject GlazzingStructure;
    public GameObject IndustrialLamp; 
    public GameObject PlantPot;
    public GameObject SoilBed;
    public GameObject MetalFrame;
    public int RowLength= 2; // Row Lenght 
    public int NumberOfRows= 2; //Number of Rows
    public int DistanceBetweenPlants= 1; //Distance Between Plants
    public int DistanceBetweenRows= 1; //Distance Between Rows

    public float IndustrialLampIntensity=1;

    public int GlassHouseperline= 1;

    public int linesofGlassHouse= 1;

    public int environment= 0;

    
    void Start()
    {
    /// Call autoGlassHouse Function (To generate a Tomato Farm Glass House environment)
       if(environment == 0){
          autoGlassHouse(NumberOfRows, RowLength);  
       }
    /// Call GenerateLand Function (To Generate a Tomato Farm without glass house elements)
       else if(environment == 1){
          GenerarLand();
       }
    /// Notify that only it is possible to choose between 0 and 1    
       else{
        Debug.LogWarning("Currently you can only select between 0 and 1"); 
       }

    }
    void GenerateGlassHouse(float g, float h)
    {
      ///////////////////ROW_LENGTH_CALIBRATION///////////////////////////////////   
        if(NumberOfRows<=19 ){
            if(DistanceBetweenRows==1){
                Debug.LogWarning("Continue ");              
                gener(RowLength, DistanceBetweenPlants, g, h);
            }
            
            if(DistanceBetweenRows==2){

                if(NumberOfRows>10){
                    Debug.LogWarning("No space for more plants ");
                }
                if(NumberOfRows<=10){
                    Debug.LogWarning("Continue ");
                    gener(RowLength, DistanceBetweenPlants, g, h);

                }
            }

            if(DistanceBetweenRows==3){
                if(NumberOfRows>7){
                    Debug.LogWarning("No space for more plants ");
                }

                if(NumberOfRows<=7){
                    Debug.LogWarning("Continue ");
                    gener(RowLength, DistanceBetweenPlants, g, h);
                }
            }

            if(DistanceBetweenRows==4){
                if(NumberOfRows>5){
                    Debug.LogWarning("No space for more plants ");  
                }

                if(NumberOfRows<=5){
                    Debug.LogWarning("Continue ");
                    gener(RowLength, DistanceBetweenPlants, g, h);

                }
                
            }
            if(DistanceBetweenRows==5 || DistanceBetweenRows==6){
                if(NumberOfRows>4){
                    Debug.LogWarning("No space for more plants ");
                    
                }

                if(NumberOfRows<=4){
                    Debug.LogWarning("Continue ");
                    gener(RowLength, DistanceBetweenPlants, g, h);
                }

            }
   
            if(DistanceBetweenRows==7 || DistanceBetweenRows==8){
                if(NumberOfRows>3){
                    Debug.LogWarning("No space for more plants ");
                    
                }

                if(NumberOfRows<=3){
                    Debug.LogWarning("Continue ");
                    gener(RowLength, DistanceBetweenPlants, g, h);
                }

            }

            if(DistanceBetweenRows==9 || DistanceBetweenRows==10 || DistanceBetweenRows==11 || DistanceBetweenRows==12|| DistanceBetweenRows==13|| DistanceBetweenRows==14|| DistanceBetweenRows==15|| DistanceBetweenRows==16|| DistanceBetweenRows==17||DistanceBetweenRows==18 ){
                if(NumberOfRows>2){
                    Debug.LogWarning("No space for more plants ");
                }

                if(NumberOfRows<=2){
                    Debug.LogWarning("Continue ");
                    gener(RowLength, DistanceBetweenPlants, g, h);
  
                }
            }
        }

        if(NumberOfRows>=19){
            Debug.LogWarning("No space for more plants ");
        }
    
    }

    void gener(int RowLength, int DistanceBetweenPlants, float g, float h)
    {
            //_____________________DISTANCE_ROWS_CALIBRATION______________________________
        if(RowLength<9 ){

            if(DistanceBetweenPlants==1){
                if(RowLength>9){
                    Debug.LogWarning("No space for more plants ");
                }
                
                if(RowLength<=9){    
                    for (int x= 0; x<NumberOfRows; x++){
                    
                        Vector3 posMetal= new Vector3(x*DistanceBetweenRows+25.3f*(g),0.1f, 0*DistanceBetweenPlants+(11.245f * h)-.5f);
                        GameObject metal = Instantiate(MetalFrame, posMetal, Quaternion.Euler(0,90,0));
                           
                        for (int z= 0; z< RowLength; z++){

                            /////////////////////////////////////////////////////////////////////
                            Vector3 posPlants= new Vector3(x*DistanceBetweenRows+25.3f*(g), 0.95f, z*DistanceBetweenPlants+(11.245f * h));
                            Vector3 posPlantPot= new Vector3(x*DistanceBetweenRows+25.3f*(g), 0.95f, z*DistanceBetweenPlants+(11.245f * h));
                            
                            GameObject nuevoTerreno = Instantiate(branch, posPlants, Quaternion.identity);
                            GameObject PlantPot1= Instantiate(PlantPot, posPlantPot, Quaternion.Euler(0,90,0));
                            
                            Vector3 posBlock= new Vector3(x*DistanceBetweenRows+25.3f*(g)+0.032f, 0.84f, z*DistanceBetweenPlants+(11.245f * h)); 
                            GameObject blockE = Instantiate(SoilBed, posBlock, Quaternion.identity); 
                             
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
                            
                            Vector3 posIndustrialLamp= new Vector3(x*DistanceBetweenRows+25.3f*(g), 4.5f, z*DistanceBetweenPlants+(11.245f * h)); 
                            GameObject IndustrialLamp1 = Instantiate(IndustrialLamp, posIndustrialLamp, Quaternion.Euler(180,90,0));
                            Light lightComponent = IndustrialLamp1.AddComponent<Light>();
                            Color colorF200FF;
                            UnityEngine.ColorUtility.TryParseHtmlString("#F200FF", out colorF200FF);
                            lightComponent.type = LightType.Point;
                            lightComponent.color = colorF200FF;

                            lightComponent.intensity = IndustrialLampIntensity;
                            
                            lightComponent.range = 10f;
                        }
                    }



                }
            }

            if(DistanceBetweenPlants==8 || DistanceBetweenPlants==7|| DistanceBetweenPlants==6|| DistanceBetweenPlants==5){
                if(RowLength>2){
                    Debug.LogWarning("No space for more plants ");
                }

                if(RowLength<=2){
                    
                    for (int x= 0; x<NumberOfRows; x++){
                        
                        Vector3 posMetal= new Vector3(x*DistanceBetweenRows+25.3f*(g), 0.1f, 0*DistanceBetweenPlants+(11.245f * h)-.5f);
                        GameObject metal = Instantiate(MetalFrame, posMetal, Quaternion.Euler(0,90,0));
                        for (int z= 0; z< RowLength; z++){
                            ////////////////////////////////////////////////////////////////////////////
                            Vector3 posPlants= new Vector3(x*DistanceBetweenRows+25.3f*(g), 0.95f, z*DistanceBetweenPlants+(11.245f * h));
                            Vector3 posPlantPot= new Vector3(x*DistanceBetweenRows+25.3f*(g), 0.95f, z*DistanceBetweenPlants+(11.245f * h));
                            GameObject nuevoTerreno = Instantiate(branch, posPlants, Quaternion.identity);
                            GameObject PlantPot1= Instantiate(PlantPot, posPlantPot, Quaternion.Euler(0,90,0));
                            
                            Vector3 posBlock= new Vector3(x*DistanceBetweenRows+25.3f*(g)+0.032f, 0.84f, z*DistanceBetweenPlants+(11.245f * h)); 
                            GameObject blockE = Instantiate(SoilBed, posBlock, Quaternion.identity); 
                            

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
                            Vector3 posIndustrialLamp= new Vector3(x*DistanceBetweenRows+25.3f*(g), 4.5f, z*DistanceBetweenPlants+(11.245f * h)); 
                            GameObject IndustrialLamp1 = Instantiate(IndustrialLamp, posIndustrialLamp, Quaternion.Euler(180,90,0));
                            Light lightComponent = IndustrialLamp1.AddComponent<Light>();
                            Color colorF200FF;
                            UnityEngine.ColorUtility.TryParseHtmlString("#F200FF", out colorF200FF);
                            lightComponent.type = LightType.Point;
                            lightComponent.color = colorF200FF;
                            lightComponent.intensity = IndustrialLampIntensity;
                            lightComponent.range = 10f;
                        }
                    }


                }

            }

            if(DistanceBetweenPlants==4 || DistanceBetweenPlants==3){
                if(RowLength>3){
                    Debug.LogWarning("No space for more plants ");
                }
                if(RowLength<=3){
                    
                    for (int x= 0; x<NumberOfRows; x++){

                        Vector3 posMetal= new Vector3(x*DistanceBetweenRows+25.3f*(g), 0.1f, 0*DistanceBetweenPlants+(11.245f * h)-.5f);
                        GameObject metal = Instantiate(MetalFrame, posMetal, Quaternion.Euler(0,90,0));
                        for (int z= 0; z< RowLength; z++){
                            /////////////////////////////////////////////////////
                            Vector3 posPlants= new Vector3(x*DistanceBetweenRows+25.3f*(g), 0.95f, z*DistanceBetweenPlants+(11.245f * h));
                            Vector3 posPlantPot= new Vector3(x*DistanceBetweenRows+25.3f*(g), 0.95f, z*DistanceBetweenPlants+(11.245f * h));
                            GameObject nuevoTerreno = Instantiate(branch, posPlants, Quaternion.identity);
                            GameObject PlantPot1= Instantiate(PlantPot, posPlantPot, Quaternion.Euler(0,90,0));
                            
                            Vector3 posBlock= new Vector3(x*DistanceBetweenRows+25.3f*(g)+0.032f, 0.84f, z*DistanceBetweenPlants+(11.245f * h)); 
                            GameObject blockE = Instantiate(SoilBed, posBlock, Quaternion.identity); 
                        
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
                            Vector3 posIndustrialLamp= new Vector3(x*DistanceBetweenRows+25.3f*(g), 4.5f, z*DistanceBetweenPlants+(11.245f * h)); 
                            GameObject IndustrialLamp1 = Instantiate(IndustrialLamp, posIndustrialLamp, Quaternion.Euler(180,90,0));
                            Light lightComponent = IndustrialLamp1.AddComponent<Light>();
                            Color colorF200FF;
                            UnityEngine.ColorUtility.TryParseHtmlString("#F200FF", out colorF200FF);
                            lightComponent.type = LightType.Point;
                            lightComponent.color = colorF200FF;
                            lightComponent.intensity = IndustrialLampIntensity;
                            lightComponent.range = 10f;
                        }
                    }

                }
                
            }

            if(DistanceBetweenPlants==2){   
                if(RowLength>5){
                    Debug.LogWarning("No space for more plants ");

                }

                if(RowLength<=5){
                    
                    for (int x= 0; x<NumberOfRows; x++){

                        Vector3 posMetal= new Vector3(x*DistanceBetweenRows+25.3f*(g), 0.1f, 0*DistanceBetweenPlants+(11.245f * h)-.5f);
                        GameObject metal = Instantiate(MetalFrame, posMetal, Quaternion.Euler(0,90,0));
                        for (int z= 0; z< RowLength; z++){
                            //////////////////////////////////////////////////////////////////////
                            Vector3 posPlants= new Vector3(x*DistanceBetweenRows+25.3f*(g), 0.95f, z*DistanceBetweenPlants+(11.245f * h));
                            Vector3 posPlantPot= new Vector3(x*DistanceBetweenRows+25.3f*(g), 0.95f, z*DistanceBetweenPlants+(11.245f * h));
                            GameObject nuevoTerreno = Instantiate(branch, posPlants, Quaternion.identity);
                            GameObject PlantPot1= Instantiate(PlantPot, posPlantPot, Quaternion.Euler(0,90,0));
                            
                            Vector3 posBlock= new Vector3(x*DistanceBetweenRows+25.3f*(g)+0.032f, 0.84f, z*DistanceBetweenPlants+(11.245f * h)); 
                            GameObject blockE = Instantiate(SoilBed, posBlock, Quaternion.identity);

                            if(x%2==0){
                                int randomIndex=  UnityEngine.Random.Range(0,4);
                                TomatoRandom(x,z, 0.55f, randomIndex, g, h);
                            }
                            if(x%2!=0){
                                int randomIndex=  UnityEngine.Random.Range(4,8);
                                TomatoRandom(x,z, 0.55f, randomIndex, g, h);
                            }
                            /////////////////////////////////////////////////////////////////////////
                            Vector3 posIndustrialLamp= new Vector3(x*DistanceBetweenRows+25.3f*(g), 4.5f, z*DistanceBetweenPlants+(11.245f * h)); 
                            GameObject IndustrialLamp1 = Instantiate(IndustrialLamp, posIndustrialLamp, Quaternion.Euler(180,90,0));
                            Light lightComponent = IndustrialLamp1.AddComponent<Light>();
                            Color colorF200FF;
                            UnityEngine.ColorUtility.TryParseHtmlString("#F200FF", out colorF200FF);
                            lightComponent.type = LightType.Point;
                            lightComponent.color = colorF200FF;
                            lightComponent.intensity = IndustrialLampIntensity;
                            lightComponent.range = 10f; 
                        }
                    }

                }
             
            }


            if(DistanceBetweenPlants<=0  || DistanceBetweenPlants>=9){
                 Debug.LogWarning("No space for more plants ");
            }

        }
        
        if (RowLength>=9){
            Debug.LogWarning("No space for more plants ");
        }
        


    }

    void GenerarLand()
    {
        earth.transform.localScale = new Vector3(DistanceBetweenRows*.1f, 1f, DistanceBetweenPlants*.1f);

        for (int x = 0; x < NumberOfRows; x++)
        {   
            for (int z = 0; z < RowLength; z++)
            {
                Vector3 pos = new Vector3(x*DistanceBetweenRows, 0, z*DistanceBetweenPlants);

                int tomatoNumber= UnityEngine.Random.Range(1,2);
                int leafNumber= UnityEngine.Random.Range(3,6);    
                int randomSpecial= UnityEngine.Random.Range(0,1);
                int randomSpecial1= UnityEngine.Random.Range(0,1);
                GameObject randomPlant = UnityEngine.Random.Range(0, 2) == 0 ? branch : branch2;
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
                
                if (randomPlant== branch){
                // 1
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 1.2f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+0.0156f, rYL, z*DistanceBetweenPlants-0.0034f);
                    GameObject leaf1 = Instantiate(leaf, posLeaf, Quaternion.identity);
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.8f, 0f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows-0.103f, randomY, z*DistanceBetweenPlants+0.025f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.identity);
                } 

                //2 
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 1.2f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+0.0258f, rYL, z*DistanceBetweenPlants-0.0117f);
                    GameObject leaf1= Instantiate(leaf, posLeaf,Quaternion.Euler(0,180,0));
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.8f, 0f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+0.091f, randomY, z*DistanceBetweenPlants-0.03f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,180,0));
                }

                //3
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 1.2f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+0.0163f,rYL,z*DistanceBetweenPlants-0.0136f);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,-90,0));
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.8f, 0f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows-0.03f, randomY, z*DistanceBetweenPlants-0.1f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,-90,0));
                }
               
                //4
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 1.2f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+0.014f,rYL,z*DistanceBetweenPlants-0.007f);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,90,0));
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.8f, 0f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+0.027f, randomY, z*DistanceBetweenPlants+0.1f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,90,0));
                    }
            
                }

                if(randomPlant== branch2){
                
                ///////1////////
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 0.848f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+0.0207f, rYL, z*DistanceBetweenPlants-0.015f);
                    GameObject leaf1 = Instantiate(leaf, posLeaf, Quaternion.identity);
                    
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.755f, -0.218f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows-0.064f, randomY, z*DistanceBetweenPlants+0.0112f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.identity);
                }


                /////////2/////////////////////////

                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 0.848f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+0.0117f, rYL, z*DistanceBetweenPlants-0.010f);
                    GameObject leaf1= Instantiate(leaf, posLeaf,Quaternion.Euler(0,180,0));
                    
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.755f, -0.218f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+0.109f, randomY, z*DistanceBetweenPlants-0.041f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,180,0));
                }

                ///////3/////////////////////////////
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 0.848f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+0.009f,rYL,z*DistanceBetweenPlants-0.005f);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,-90,0));
                    
                }
                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.755f, -0.218f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows-0.008f, randomY, z*DistanceBetweenPlants-0.081f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,-90,0));
                }

                /////////////////4//////////////////////
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 0.848f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+0.011f,rYL,z*DistanceBetweenPlants-0.001f);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,90,0));  
                                    
                    }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(-0.755f, -0.218f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+0.0689f, randomY, z*DistanceBetweenPlants+0.0533f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,90,0));
                    
                }

                //Extra tomato
            
                for(int t=0; t<randomSpecial;t++){
                    float randomY = UnityEngine.Random.Range(-0.210f, -0.232f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+0.3164f, randomY, z*DistanceBetweenPlants+0.083f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,90,0));
                }

                for(int t=0; t<randomSpecial1;t++){
                    float randomY = UnityEngine.Random.Range(-0.126f, -0.130f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+0.209f, randomY, z*DistanceBetweenPlants-0.097f); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,-90,0));
                }
        
                }
                //Leaf modifications
                for(int l=0; l<0; l++){
                    float rYL = UnityEngine.Random.Range(0.14f, 0.848f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows,rYL,z*DistanceBetweenPlants);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,90,0));  
                                    
                    }

                    
            }
        }
    }

    void TomatoRandom(float x, float z, float y, int randomIndex, float g, float h){
                Vector3 posPlants= new Vector3(x*DistanceBetweenRows+25.3f*(g), 0.55f, z*DistanceBetweenPlants);

                int tomatoNumber= UnityEngine.Random.Range(0,2);
                int leafNumber= UnityEngine.Random.Range(3,8);    
                int randomSpecial= UnityEngine.Random.Range(0,1);
                int randomSpecial1= UnityEngine.Random.Range(0,1);
                GameObject randomPlant = UnityEngine.Random.Range(0, 2) == 0 ? branch : branch2;
                GameObject randomTomatoe = optionTomatoes[randomIndex];

                
                if (randomPlant== branch){
                // 1
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+1.2f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+25.3f*(g), rYL+0.5f, z*DistanceBetweenPlants+11.245f * h);
                    GameObject leaf1 = Instantiate(leaf, posLeaf, Quaternion.identity);
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.8f, y+0f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+25.3f*(g), randomY+0.5f, z*DistanceBetweenPlants+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.identity);
                } 

                //2 
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+1.2f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+25.3f*(g), rYL+0.5f, z*DistanceBetweenPlants+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf,Quaternion.Euler(0,180,0));
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.8f, y+0f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+25.3f*(g), randomY+0.5f, z*DistanceBetweenPlants+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,180,0));
                }

                //3
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+1.2f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+25.3f*(g),rYL+0.5f,z*DistanceBetweenPlants+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,-90,0));
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.8f, y+0f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+25.3f*(g), randomY+0.5f, z*DistanceBetweenPlants+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,-90,0));
                }
               
                //4
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+1.2f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+25.3f*(g),rYL+0.5f,z*DistanceBetweenPlants+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,90,0));
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.8f, y+0f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+25.3f*(g), randomY+0.5f, z*DistanceBetweenPlants+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,90,0));
                    }
            
                }

                if(randomPlant== branch2){
                
                ///////1////////
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+0.848f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+25.3f*(g), rYL+0.5f, z*DistanceBetweenPlants+11.245f * h);
                    GameObject leaf1 = Instantiate(leaf, posLeaf, Quaternion.identity);
                    
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.755f, y+-0.218f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+25.3f*(g), randomY+0.5f, z*DistanceBetweenPlants+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.identity);
                }


                /////////2/////////////////////////

                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+0.848f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+25.3f*(g), rYL+0.5f, z*DistanceBetweenPlants+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf,Quaternion.Euler(0,180,0));
                    
                }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.755f, y+-0.218f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+25.3f*(g), randomY+0.5f, z*DistanceBetweenPlants+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,180,0));
                }

                ///////3/////////////////////////////
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+0.848f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+25.3f*(g),rYL+0.5f,z*DistanceBetweenPlants+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,-90,0));
                    
                }
                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.755f, y+-0.218f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+25.3f*(g), randomY+0.5f, z*DistanceBetweenPlants+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,-90,0));
                }

                /////////////////4//////////////////////
                for(int l=0; l<leafNumber; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+0.848f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+25.3f*(g),rYL+0.5f,z*DistanceBetweenPlants+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,90,0));  
                                    
                    }

                for(int t=0; t<tomatoNumber;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.755f, y+-0.218f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+25.3f*(g), randomY+0.5f, z*DistanceBetweenPlants+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,90,0));
                    
                }

                //Extra tomato
            
                for(int t=0; t<randomSpecial;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.210f, y+-0.232f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+25.3f*(g), randomY+0.5f, z*DistanceBetweenPlants+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,90,0));
                }

                for(int t=0; t<randomSpecial1;t++){
                    float randomY = UnityEngine.Random.Range(y+-0.126f, y+-0.130f);
                    Vector3 posTomato = new Vector3(x*DistanceBetweenRows+25.3f*(g), randomY+0.5f, z*DistanceBetweenPlants+11.245f * h); //De -0.8f a 0f
                    GameObject tomato = Instantiate(randomTomatoe, posTomato, Quaternion.Euler(0,-90,0));
                }
        
                }
                //Leaf modifications
                for(int l=0; l<0; l++){
                    float rYL = UnityEngine.Random.Range(y+0.14f, y+0.848f);
                    Vector3 posLeaf= new Vector3(x*DistanceBetweenRows+25.3f*(g),rYL+0.5f,z*DistanceBetweenPlants+11.245f * h);
                    GameObject leaf1= Instantiate(leaf, posLeaf, Quaternion.Euler(0,90,0));               
                    }
    }



    void LandTomatoes(){

        earth.transform.localScale = new Vector3(DistanceBetweenRows*.1f, 1f, DistanceBetweenPlants*.1f);

        for (int x = 0; x < NumberOfRows; x++)
        {   
            for (int z = 0; z < RowLength; z++)
            {
                Vector3 pos = new Vector3(x*DistanceBetweenRows, 0, z*DistanceBetweenPlants);
                GameObject nuevoTerreno = Instantiate(branch, pos, Quaternion.identity);
                GameObject planeEarth = Instantiate(earth, pos, Quaternion.identity);
                
            }
        }

    }

    void oneGlassHouse(){
        for (int g=0; g<GlassHouseperline; g++){
            for (int h=0; h<linesofGlassHouse; h++){
                Vector3 posGlazzingStructure= new Vector3(10*g*2.5f, 0, 17.3f*h*0.65f);
                GameObject GlazzingStructureGlassHouse= Instantiate(GlazzingStructure, posGlazzingStructure, Quaternion. identity);
            }
        }
    }
    void autoGlassHouse(int NumberOfRows, int RowLength) {
        // Control the number of glazing structures that are going to be spawned per line
        for (int g = 0; g < GlassHouseperline; g++) {
            // Control the number of lines of glazing structures that are going to be spawned
            for (int h = 0; h < linesofGlassHouse; h++) {
                // Calculate the position of each glazing structure based on its row (h) and column (g)
                Vector3 posGlazingStructure = new Vector3(25.3f * g, 0, 11.245f * h+0.6f);
                
                // Instantiate a new glazing structure game object at the calculated position
                GameObject GlazingStructureGlassHouse = Instantiate(GlazzingStructure, posGlazingStructure, Quaternion.Euler(0, 180, 0));
                
                // Call the GenerateGlassHouse function with the current indices (g, h) 
                GenerateGlassHouse(g, h); 
            }
        }
    }

          
}



