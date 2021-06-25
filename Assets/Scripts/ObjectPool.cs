using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    //static instance
    private static ObjectPool PoolInstance;
    //static parameter
    public static ObjectPool Instance
    {
        //Fields can be made read-only(if you only use the get method), or write-only(if you only use the set method)
        //Better control of class members (reduce the possibility of yourself(or others) to mess up the code)
        //Flexible: the programmer can change one part of the code without affecting other parts
        //Increased security of data
        get
        {
            if (PoolInstance == null)
            {
                PoolInstance = new ObjectPool();
            }
            return PoolInstance;
        }
    }

    //Dictionary<TKey, TValue> stores key-value pairs.
    //Comes under System.Collections.Generic namespace.
    //Implements IDictionary<TKey, TValue> interface.
    //Keys must be unique and cannot be null.
    //Values can be null or duplicate.
    //Values can be accessed by passing associated key in the indexer e.g.myDictionary[key]
    //Elements are stored as KeyValuePair<TKey, TValue> objects.

    //Queue<T> is FIFO (First In First Out) collection.
    //It comes under System.Collection.Generic namespace.
    //Queue<T> can contain elements of the specified type.It provides compile-time type checking and doesn't perform boxing-unboxing because it is generic.
    //Elements can be added using the Enqueue() method.Cannot use collection-initializer syntax.
    //Elements can be retrieved using the Dequeue() and the Peek() methods.It does not support an indexer.

    private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

    public GameObject QueueOut(GameObject prefab)
    {
        GameObject _prefab;

        //QueueCheck: 
        //如果沒有receivedPrefab的queue，或者queue裡沒人，QueueSet and QueueIn
        if (!poolDictionary.ContainsKey(prefab.name) || poolDictionary[prefab.name].Count == 0)
        {
            QueueSet(prefab.name);

            _prefab = GameObject.Instantiate(prefab); //Instantiate一個複製體(名字後面會有(Clone))
            QueueIn(_prefab);

            _prefab.transform.SetParent(subjectPool.transform); //attach在subjectPool的下一層
        }

        _prefab = poolDictionary[prefab.name].Dequeue(); //把Queue裡的第一個GameObject prefab拿出來
        _prefab.SetActive(true); //設為active
        return _prefab; //回傳
    }

    private GameObject objectPool;
    private GameObject subjectPool;
    
    //QueueSet: 
    //建立主池"Object Pool", 建立副池"物件名稱 Pool", 建立物件名稱的Queue
    public void QueueSet(string _name)
    {
        //如果沒有已建立的頂層pool "Object Pool"，建立一個
        if (objectPool == null)
        {
            objectPool = new GameObject("Object Pool");
        }

        //如果沒有已建立的子類型的pool，建一個prefeb名字的pool，attach在objectPool的下一層
        subjectPool = GameObject.Find(_name + " Pool");

        if (!subjectPool)
        {
            subjectPool = new GameObject(_name + " Pool");
            subjectPool.transform.SetParent(objectPool.transform);
        }

        //如果沒有已建立的Queue，創一個新的
        if (!poolDictionary.ContainsKey(_name)) 
        {
            poolDictionary.Add(_name, new Queue<GameObject>()); 
        }   
    }

    //QueueIn: 
    //Deactivate丟進Queue
    public void QueueIn(GameObject _prefab)
    {
        string _name = _prefab.name.Replace("(Clone)", string.Empty);

        _prefab.SetActive(false);
        poolDictionary[_name].Enqueue(_prefab);
    }
}

