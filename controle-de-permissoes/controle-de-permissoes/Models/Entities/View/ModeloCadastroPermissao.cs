﻿using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.Entities.View {
    public class ModeloCadastroPermissao {
        public Permissao Permissao { get; set; }

        public List<Sistema> TodosSistemas { get; set; }

        public List<Permissao> TodasPermissoes { get; set; }
    }
}
