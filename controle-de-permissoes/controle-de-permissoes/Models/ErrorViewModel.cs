namespace controle_de_permissoes.Models {
    public class ErrorViewModel {
        public string? RequestId {
            get; set;
        }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}