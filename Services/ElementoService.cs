using aplicacionExamen.Data;
using aplicacionExamen.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace aplicacionExamen.Services
{
    public class ElementoService
    {



        private readonly ElementoDbContext _context;



        // Constructor que recibe el contexto de la base de datos
        public ElementoService(ElementoDbContext context)
        {
            _context = context;
        }




        // Método para obtener todos los clientes de la base de datos utilizando un procedimiento almacenado
        public async Task<List<Elemento>> GetAllElementsAsync()
        {
            List<Elemento> elementos = new List<Elemento>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "GetAllElements"; // Nombre del procedimiento almacenado
                command.CommandType = CommandType.StoredProcedure;

                await _context.Database.OpenConnectionAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Elemento elemento = new Elemento
                        {
                            Id = reader.GetInt32(0), // Ajusta el índice según el orden de las columnas en el resultado del procedimiento almacenado
                            Name = reader.GetString(1), // Ajusta el índice según el orden de las columnas en el resultado del procedimiento almacenado
                            Description = reader.GetString(2), // Ajusta el índice según el orden de las columnas en el resultado del procedimiento almacenado
                            Telefono = reader.GetString(3), // Índice 3: Telefono
                             Pais = reader.GetString(4), // Índice 4: Pais
                    };

                        elementos.Add(elemento);
                    }
                }
            }

            return elementos;
        }




        // Método para agregar un nuevo elemento a la base de datos
        public async Task AddElementAsync(Elemento element)
        {
            _context.Elements.Add(element); // Agrega el elemento al contexto
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }


        //Ahora, el controlador puede llamar a este método dentro de la acción DeleteElement para obtener el elemento que se eliminará.
        public async Task<Elemento> GetElementByIdAsync(int id)
        {
            return await _context.Elements.FindAsync(id);
        }


        // Método para editar un elemento existente en la base de datos
        public async Task EditarElementoAsync(Elemento element)
        {
            _context.Elements.Update(element); // Marca el elemento como modificado en el contexto
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }




        // Método para eliminar un elemento de la base de datos
        public async Task EliminarElementoAsync(int elementoId)
        {
            var elemento = await _context.Elements.FindAsync(elementoId); // Busca el elemento por su ID
            if (elemento != null)
            {
                _context.Elements.Remove(elemento); // Elimina el elemento del contexto
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
        }
    }
}
