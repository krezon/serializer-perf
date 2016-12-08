﻿using System.IO;
using System.Runtime.CompilerServices;
using GroBuf;
using GroBuf.DataMembersExtracters;
using Jil;
using MsgPack.Serialization;
using Newtonsoft.Json;
using ServiceStack;
using SerializationContext = MsgPack.Serialization.SerializationContext;
using ProtoSerializer = ProtoBuf.Serializer;

namespace Serializers
{
    public class MixedSerializer<T>
    {
        private readonly MessagePackSerializer<T> _mgsPackSerializer;
        private readonly ISerializer _groBuf;
        private readonly Wire.Serializer _wireSerializer;

        public MixedSerializer()
        {
            _mgsPackSerializer = SerializationContext.Default.GetSerializer<T>();
            _groBuf = new GroBuf.Serializer(new PropertiesExtractor(), options: GroBufOptions.WriteEmptyObjects);
            _wireSerializer = new Wire.Serializer();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Pack(Stream s, T o)
        {
            _mgsPackSerializer.Pack(s, o);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Unpack(Stream input)
        {
            return _mgsPackSerializer.Unpack(input);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ProtoSerialize(Stream s, T data)
        {
            ProtoSerializer.Serialize(s, data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T ProtoDeserialize(Stream s)
        {
            return ProtoSerializer.Deserialize<T>(s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string JsonNetSerialize(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T JsonNetDeserialize(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string JilSerialize(T data)
        {
            return JSON.Serialize(data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T JilDeserialize(string input)
        {
            return JSON.Deserialize<T>(input);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte[] GroBufSerialize(T data)
        {
            return _groBuf.Serialize(data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GroBufDeserialize(byte[] input)
        {
            return _groBuf.Deserialize<T>(input);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string FastJsonSerialize(T data)
        {
            return fastJSON.JSON.ToJSON(data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T FastJsonDeserialize(string input)
        {
            return fastJSON.JSON.ToObject<T>(input);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ServiceStackJsonSerializer(T data)
        {
            return data.ToJson();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T ServiceStackJsonDeserializer(string input)
        {
            return input.FromJson<T>();
        }

        /// <summary>
        /// https://github.com/rogeralsing/Wire
        /// </summary>
        /// <param name="s">Stream</param>
        /// <param name="data">Input data</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WireSerialize(Stream s, T data)
        {
            _wireSerializer.Serialize(data, s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T WireDeserialize(Stream input)
        {
            return _wireSerializer.Deserialize<T>(input);
        }
    }
}
