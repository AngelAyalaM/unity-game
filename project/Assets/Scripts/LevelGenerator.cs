using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{   
    public List<LevelBlock> legoBlock = new List<LevelBlock>();
    List<LevelBlock> currentBlock = new List<LevelBlock>();
    public Transform initialPoint;
    private static LevelGenerator _sharedInstance; 

    public static LevelGenerator sharedInstance{
        get{
            return _sharedInstance;
        }
    }
    public byte initialBlockNumber = 2;
    private void Awake() {
        _sharedInstance = this;
       createInitialBlocks();
       

    }
    public void createInitialBlocks(){
        if (currentBlock.Count > 0)
        {
            return;
        }
        for (byte i = 0; i < initialBlockNumber; i++)
        {
            AddNewBlock(true);
        }
    }  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddNewBlock(bool initialBlock = false)
    {
         int randNumber  =initialBlock? 0: Random.Range(0, legoBlock.Count );
         //var myBlock = new LevelBlock();
         var block = Instantiate(legoBlock[randNumber]); 
         block.transform.SetParent(this.transform, false);
         Vector3 blockPosition = Vector3.zero;
         if (currentBlock.Count == 0)
         {
            blockPosition = initialPoint.position;
         }else
         {
            int lastBlockPos = currentBlock.Count - 1;
            blockPosition = currentBlock[lastBlockPos].exitPoint.position; 
         }
         block.transform.position = blockPosition;
         currentBlock.Add(block); 
          
    }
    public void RemoveOldBlock(){
        var oldblock = currentBlock[0];
        currentBlock.Remove(oldblock);
        Destroy(oldblock.gameObject);
    }
    public void RemoveAllBlocks()
    {
        while (currentBlock.Count > 0)
        {
            RemoveOldBlock();
        }
    }
}
