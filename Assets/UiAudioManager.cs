using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAudioManager : MonoBehaviour
{
    [SerializeField] GameObject grainUI;

    private List<Grain> sentGrainData;

    //todo: beide arrays durch HashMaps (Dictionary) ersetzen, key = id
    public Dictionary<int, float> positions;
    public Dictionary<int, uint> lengths;
    public List<DictionaryHelper> dictionayHelperPositions;
    public List<DictionaryHelper> dictionayHelperLengths;


    // public float[] receivedPositions;
    //   public uint[] receivedLengths;
    public int numGrains;
    public uint samplerLength;
    private static UiAudioManager _instance;
    public static UiAudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("AudioLoader");
                go.AddComponent<UiAudioManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        positions = new Dictionary<int, float>();
        lengths = new Dictionary<int, uint>();
        dictionayHelperPositions = new List<DictionaryHelper>();
        dictionayHelperLengths = new List<DictionaryHelper>();
    }

    private void Start()
    {
        _instance = this;
        // SoundSampler.OnNewGrainsCreated += InitializeData;
        //SoundSampler.OnNewGrainsCreated += CreateGrainUI;

        SoundSampler.OnNewGrainCreated += InitializeData;
        SoundSampler.OnNewGrainCreated += CreateGrainUI;

        SoundSampler.OnGrainDestroyed += InitializeData2;
        SoundSampler.OnGrainDestroyed += DestroyGrainUI;

       
    }

    void Update()
    {
        ReceiveSamplerData();
        UpdateDictionaryHelper();
    }

    void InitializeData(Grain g)
    {   /*
        samplerLength = (uint)(SoundSampler.Instance.sampleLength);
        sentGrainData = SoundSampler.Instance.GetGrains(); 
        numGrains = sentGrainData.Count;
        receivedPositions = new float[numGrains];
        receivedLengths = new uint[numGrains];
        */
        samplerLength = (uint)(SoundSampler.Instance.sampleLength);
        numGrains = SoundSampler.Instance.GetGrains().Count;
        positions.Add(g.grainId, g.GetGrainPosition());
        lengths.Add(g.grainId, (uint) g.GetGrainLength());

    }

    void InitializeData2(Grain g)
    {    
        numGrains = SoundSampler.Instance.GetGrains().Count;
        positions.Remove(g.grainId);
        lengths.Remove(g.grainId);
    }

    void ReceiveSamplerData()
    {
        /*
        for (int i = 0; i < numGrains; i++)
        {
            receivedPositions[i] = sentGrainData[i].GetGrainPosition();
            receivedLengths[i] = (uint)sentGrainData[i].GetGrainLength();
        }
        */
        foreach(Grain g in SoundSampler.Instance.GetGrains())
        {
            positions[g.grainId] = g.GetGrainPosition();
            lengths[g.grainId] = (uint) g.GetGrainLength();
        }
    }

    public static float GetSampelerWidth()
    {
        return _instance.GetComponent<RectTransform>().rect.width;
    }

    void CreateGrainUI(Grain g)
    {
        //   GameObject go = GameObject.Instantiate(grainUI);
        //   go.GetComponent<GrainData>().SetId(g.grainId);

        GameObject go = UI_Pool.Instance.pool.Dequeue();
        go.GetComponent<GrainData>().SetId(g.GetGrainId());
        UI_Pool.Instance.activeObjects.Add(go);

        UI_Pool.Instance.UpdatePoolList();
        go.SetActive(true);
        print(go.name + " added to activeList");

    }

    void DestroyGrainUI(Grain g)
    {
        GameObject go = UI_Pool.Instance.activeObjects.Find(x => x.GetComponent<GrainData>().id == g.GetGrainId());
        UI_Pool.Instance.pool.Enqueue(go);
        UI_Pool.Instance.activeObjects.Remove(go);
        UI_Pool.Instance.UpdatePoolList();
        go.SetActive(false);
        print(go.name + " added to pool");

    }

    //sehr ineffizient, temp
    void UpdateDictionaryHelper()
    {
      dictionayHelperLengths.Clear();
        dictionayHelperPositions.Clear();
        foreach(Grain g in SoundSampler.Instance.GetGrains())
        {
         dictionayHelperLengths.Add(new DictionaryHelper(g.grainId, g.GetGrainLength()));
            dictionayHelperPositions.Add(new DictionaryHelper(g.grainId, g.GetGrainPosition()));
        }

    }
}
