import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

function Login() {
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [message, setMessage] = useState(""); // Mensagem de feedback
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const handleLogin = async () => {
        setLoading(true);
        setMessage("");

        try {
            const response = await axios.post("http://localhost:5000/api/auth/login", { email, senha });
            localStorage.setItem("token", response.data.token);
            setMessage("Login realizado com sucesso!");
            setTimeout(() => navigate("/dashboard"), 1000);
        } catch (error) {
            setMessage("Erro ao fazer login. Verifique suas credenciais.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div>
            <h1>Login</h1>
            {message && <p>{message}</p>}
            <input
                type="email"
                placeholder="E-mail"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
            />
            <input
                type="password"
                placeholder="Senha"
                value={senha}
                onChange={(e) => setSenha(e.target.value)}
            />
            <button onClick={handleLogin} disabled={loading}>
                {loading ? "Entrando..." : "Entrar"}
            </button>
            <p>Ainda não tem conta? <a href="/register">Cadastre-se</a></p>
        </div>
    );
    
}

export default Login;