using UnityEngine;
using System;

namespace Overdose.Data
{
    /// <summary>�v�[������I�u�W�F�N�g���i�[�����X�N���v�^�u���I�u�W�F�N�g</summary>
    [CreateAssetMenu(fileName = "ObjectsPoolData")]
    public class ObjectsPoolData : ScriptableObject
    {
        public ObjectData[] Data => data;

        [SerializeField]
        private ObjectData[] data = default;

        /// <summary>�v�[������I�u�W�F�N�g�̈��f�[�^���i�[�����N���X</summary>
        [Serializable]
        public class ObjectData
        {
            public GameObject Prefab { get => objectPrefab; }
            public PoolObjectType Type { get => objectType; }
            public int MaxCount { get => objectMaxCount; }
            [SerializeField]
            private string Name;
            [SerializeField]
            private PoolObjectType objectType;
            [SerializeField]
            private GameObject objectPrefab;
            [SerializeField]
            private int objectMaxCount;
        }
    }
}