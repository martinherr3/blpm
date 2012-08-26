///////////////////////////////////////////////////////////
//  Cargo.cs
//  Implementation of the Class Cargo
//  Generated by Enterprise Architect
//  Created on:      14-jun-2011 20:12:10
//  Original author: Laura Pastorino
///////////////////////////////////////////////////////////

using System;
using EDUAR_Entities.Shared;
namespace EDUAR_Entities
{
    [Serializable]
    public class CargoPersonal: DTBase
    {
        public bool activo { get; set; }
        public int idCargo { get; set; }
        public int idCargoTransaccional { get; set; }
        public string descripcion { get; set; }
        public string nombre { get; set; }

        public CargoPersonal()
        {

        }

        ~CargoPersonal()
        {

        }

        public virtual void Dispose()
        {

        }
    }//end Cargo
}