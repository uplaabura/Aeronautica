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
        //�p�G�S��receivedPrefab��queue�A�Ϊ�queue�̨S�H�AQueueSet and QueueIn
        if (!poolDictionary.ContainsKey(prefab.name) || poolDictionary[prefab.name].Count == 0)
        {
            QueueSet(prefab.name);

            _prefab = GameObject.Instantiate(prefab); //Instantiate�@�ӽƻs��(�W�r�᭱�|��(Clone))
            QueueIn(_prefab);

            _prefab.transform.SetParent(subjectPool.transform); //attach�bsubjectPool���U�@�h
        }

        _prefab = poolDictionary[prefab.name].Dequeue(); //��Queue�̪��Ĥ@��GameObject prefab���X��
        _prefab.SetActive(true); //�]��active
        return _prefab; //�^��
    }

    private GameObject objectPool;
    private GameObject subjectPool;
    
    //QueueSet: 
    //�إߥD��"Object Pool", �إ߰Ʀ�"����W�� Pool", �إߪ���W�٪�Queue
    public void QueueSet(string _name)
    {
        //�p�G�S���w�إߪ����hpool "Object Pool"�A�إߤ@��
        if (objectPool == null)
        {
            objectPool = new GameObject("Object Pool");
        }

        //�p�G�S���w�إߪ��l������pool�A�ؤ@��prefeb�W�r��pool�Aattach�bobjectPool���U�@�h
        subjectPool = GameObject.Find(_name + " Pool");

        if (!subjectPool)
        {
            subjectPool = new GameObject(_name + " Pool");
            subjectPool.transform.SetParent(objectPool.transform);
        }

        //�p�G�S���w�إߪ�Queue�A�Ф@�ӷs��
        if (!poolDictionary.ContainsKey(_name)) 
        {
            poolDictionary.Add(_name, new Queue<GameObject>()); 
        }   
    }

    //QueueIn: 
    //Deactivate��iQueue
    public void QueueIn(GameObject _prefab)
    {
        string _name = _prefab.name.Replace("(Clone)", string.Empty);

        _prefab.SetActive(false);
        poolDictionary[_name].Enqueue(_prefab);
    }
}

