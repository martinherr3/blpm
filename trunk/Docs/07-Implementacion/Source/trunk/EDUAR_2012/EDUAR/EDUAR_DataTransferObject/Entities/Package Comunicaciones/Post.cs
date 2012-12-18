///////////////////////////////////////////////////////////
//  Post.cs
//  Implementation of the Class Post
//  Generated by Enterprise Architect
//  Created on:      20-jun-2011 16:21:54
//  Original author: Pablo Nicoliello
///////////////////////////////////////////////////////////


using System;
using EDUAR_Entities.Shared;
namespace EDUAR_Entities
{
    [Serializable]
    public class Post:DTBase
    {
        public int idPost { get; set; }
        public DateTime fechaPost { get; set; }
        public string textoPost { get; set; }
        public Usuario usuario { get; set; }

        public Post()
        {

        }

        ~Post()
        {

        }

        public virtual void Dispose()
        {

        }

    }//end Post
}