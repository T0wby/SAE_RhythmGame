using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioManaging
{
    public class EntityMusicRequest : AudioRequest<ESources, EMusicTypes>
    {
        public bool IsRandomizable { get; }
        public Transform Parent { get; }
        public Vector3 Position { get; }

        public static EntityMusicRequest Request(ESources _source, EMusicTypes _type, Transform _parent)
        {
            return new EntityMusicRequest(_source, _type, _parent, _parent.position, false);
        }
        private EntityMusicRequest(ESources _source, EMusicTypes _type, Transform _parent, Vector3 _pos, bool _rnd)
                : base(_source, _type)
        {
            Parent = _parent;
            Position = _pos;
            IsRandomizable = _rnd;
        }
    } 
}
