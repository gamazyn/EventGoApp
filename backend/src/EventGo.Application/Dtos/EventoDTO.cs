using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventGo.Application.Dtos
{
    public class EventoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Local { get; set; }

        [Display(Name = "Data Evento")]
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        StringLength(50, MinimumLength = 4, ErrorMessage = "{0} deve conter entre 4 e 50 caracteres.")]
        public string Tema { get; set; }

        [Display(Name = "Qtd Pessoas"),
        Range(5, 10000, ErrorMessage = "{0} deve ser entre 5 e 10000.")]
        public int QtdPessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$",
        ErrorMessage = "Não é uma imagem válida. Formatos aceitos: gif, jpg, jpeg, bmp e png.")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório."),
        Phone(ErrorMessage = "O campo {0} deve conter um número de contato válido")]
        public string Telefone { get; set; }

        [Display(Name = "e-mail"),
        EmailAddress(ErrorMessage = "Precisa ser um {0} válido."),
        Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Email { get; set; }
        public int UserId { get; set; }
        public UserDTO UserDTO { get; set; }
        public IEnumerable<LoteDTO> Lotes { get; set; }

        public IEnumerable<RedeSocialDTO> RedeSociais { get; set; }

        public IEnumerable<OrganizadorDTO> Organizadores { get; set; }
    }
}