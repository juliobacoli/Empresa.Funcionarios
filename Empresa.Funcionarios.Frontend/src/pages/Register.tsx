import { useState } from "react";

function Register() {
    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");


    return (
        <div>
            <h1>Cadastro</h1>
            <p style={{ color: "red", fontWeight: "bold" }}>
                Funcionalidade indisponível no momento.
            </p>
            <form>
                <input
                    type="text"
                    placeholder="Nome"
                    value={nome}
                    onChange={(e) => setNome(e.target.value)}
                    required
                />
                <input
                    type="email"
                    placeholder="E-mail"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                />
                <input
                    type="password"
                    placeholder="Senha"
                    value={senha}
                    onChange={(e) => setSenha(e.target.value)}
                    required
                />
                <button
                    type="button"
                    disabled
                    style={{
                        cursor: "not-allowed",
                        backgroundColor: "#ccc",
                        color: "#666",
                        border: "none",
                        padding: "10px",
                        marginTop: "10px",
                    }}
                    title="Funcionalidade indisponível"
                >
                    Cadastrar
                </button>
            </form>
            <p>Já tem uma conta? <a href="/login">Faça login</a></p>
        </div>
    );
}

export default Register;