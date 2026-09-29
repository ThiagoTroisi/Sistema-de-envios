using BE;
using BLL.Otros;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BLL
{
    public class PersonaBLL
    {
        private PersonaDAL dal = new PersonaDAL();
        private EventoBLL eventobll = new EventoBLL();
        private DVBLL dvBLL = new DVBLL();

        public PersonaBE ConsultaPorDNI(int dni)
        {
            if (dni <= 0) throw new Exception("El DNI ingresado no es válido.");

            return dal.ConsultaPorDNI(dni);
        }

        public List<PersonaBE> ObtenerPersonas(bool todos)
        {
            return dal.ObtenerPersonas(todos);
        }

        public void AltaPersona(PersonaBE persona)
        {
            ValidarPersona(persona);

            PersonaBE existente = dal.ConsultaPorDNI(persona.DNI);

            if (existente != null)
            {
                if (dal.EstaActiva(persona.DNI)) throw new Exception("Ya existe una persona registrada con ese DNI.");

                throw new Exception("La persona se encuentra dada de baja.");
            }

            dal.AltaPersona(persona);

            dvBLL.ActualizarDVH("Persona", "dni", persona.DNI);
            dvBLL.ActualizarDVV("Persona");

            eventobll.RegistrarEvento("maestro_personas", "ev_alta_persona", 2);
        }

        public void ModificarPersona(PersonaBE persona)
        {
            ValidarPersona(persona);

            PersonaBE existente = dal.ConsultaPorDNI(persona.DNI);

            if (existente == null) throw new Exception("No existe una persona registrada con ese DNI.");

            if (!dal.EstaActiva(persona.DNI)) throw new Exception("No se puede modificar una persona que se encuentra dada de baja.");

            dal.ModificarPersona(persona);

            dvBLL.ActualizarDVH("Persona", "dni", persona.DNI);
            dvBLL.ActualizarDVV("Persona");

            eventobll.RegistrarEvento("maestro_personas", "ev_modificacion_persona", 3);
        }

        public void BajaPersona(int dni)
        {
            if (dni <= 0) throw new Exception("El DNI ingresado no es válido.");

            PersonaBE persona = dal.ConsultaPorDNI(dni);

            if (persona == null) throw new Exception("No existe una persona registrada con ese DNI.");

            if (!dal.EstaActiva(dni)) throw new Exception("La persona ya se encuentra dada de baja.");

            dal.BajaPersona(dni);

            dvBLL.ActualizarDVH("Persona", "dni", dni);
            dvBLL.ActualizarDVV("Persona");

            eventobll.RegistrarEvento("maestro_personas", "ev_baja_persona", 2);
        }

        public void ReactivarPersona(int dni)
        {
            if (dni <= 0) throw new Exception("El DNI ingresado no es válido.");

            PersonaBE persona = dal.ConsultaPorDNI(dni);

            if (persona == null) throw new Exception("No existe una persona registrada con ese DNI.");

            if (dal.EstaActiva(dni)) throw new Exception("La persona ya se encuentra activa.");

            dal.ReactivarPersona(dni);

            dvBLL.ActualizarDVH("Persona", "dni", dni);
            dvBLL.ActualizarDVV("Persona");

            eventobll.RegistrarEvento("maestro_personas", "ev_reactivar_persona", 2);
        }

        private void ValidarPersona(PersonaBE persona)
        {
            if (persona == null) throw new Exception("La persona no puede ser nula.");

            if (persona.DNI < 1000000 || persona.DNI > 99999999) throw new Exception("El DNI debe contener entre 7 y 8 dígitos.");

            if (string.IsNullOrWhiteSpace(persona.Nombre)) throw new Exception("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(persona.Apellido)) throw new Exception("El apellido es obligatorio.");

            if (string.IsNullOrWhiteSpace(persona.Telefono)) throw new Exception("El teléfono es obligatorio.");
            
            if (!FormatoTelefono(persona.Telefono)) throw new Exception("El formato del teléfono no es válido. Ingréselo sin espacios ni símbolos especiales");

            if (string.IsNullOrWhiteSpace(persona.Email)) throw new Exception("El email es obligatorio.");

            if (!FormatoMail(persona.Email)) throw new Exception("El formato del email no es válido.");
        }

        private bool FormatoMail(string email)
        {
            string formato = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool valido = Regex.IsMatch(email, formato);
            return valido;
        }
        private bool FormatoTelefono(string telefono)
        {
            string formato = @"^(11|15)\d{8}$|^(2224|2225)\d{6}$";
            bool valido = Regex.IsMatch(telefono, formato);
            return valido;
        }
        public bool EstaActiva(int dni)
        {
            if (dni <= 0)
                throw new Exception("El DNI ingresado no es válido.");

            return dal.EstaActiva(dni);
        }
    }
}