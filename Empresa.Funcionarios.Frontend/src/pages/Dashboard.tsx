import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

function Dashboard() {
    const [funcionarios, setFuncionarios] = useState([]);
    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [editandoId, setEditandoId] = useState<string | null>(null);
    const [message, setMessage] = useState(""); // Mensagem de feedback
    const navigate = useNavigate();

    useEffect(() => {
        fetchFuncionarios();
    }, []);

    const fetchFuncionarios = async () => {
        try {
            const token = localStorage.getItem("token");
            if (!token) {
                navigate("/login");
                return;
            }

            const response = await axios.get("http://localhost:5000/api/funcionarios", {
                headers: { Authorization: `Bearer ${token}` }
            });

            setFuncionarios(response.data);
        } catch (error) {
            console.error("Erro ao buscar funcionários", error);
        }
    };

    const handleLogout = () => {
        localStorage.removeItem("token");
        navigate("/login");
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        const token = localStorage.getItem("token");

        try {
            if (editandoId) {
                await axios.put(`http://localhost:5000/api/funcionarios/${editandoId}`, { nome, email }, {
                    headers: { Authorization: `Bearer ${token}` }
                });
                setMessage("Funcionário atualizado com sucesso!");
                setEditandoId(null);
            } else {
                await axios.post("http://localhost:5000/api/funcionarios", { nome, email }, {
                    headers: { Authorization: `Bearer ${token}` }
                });
                setMessage("Funcionário adicionado com sucesso!");
            }

            setNome("");
            setEmail("");
            fetchFuncionarios();
        } catch (error) {
            setMessage("Erro ao salvar funcionário.");
            console.error("Erro ao salvar funcionário", error);
        }
    };

    const handleEdit = (funcionario: any) => {
        setEditandoId(funcionario.id);
        setNome(funcionario.nome);
        setEmail(funcionario.email);
    };

    const handleDelete = async (id: string) => {
        const token = localStorage.getItem("token");

        try {
            await axios.delete(`http://localhost:5000/api/funcionarios/${id}`, {
                headers: { Authorization: `Bearer ${token}` }
            });
            setMessage("Funcionário excluído com sucesso!");
            fetchFuncionarios();
        } catch (error) {
            setMessage("Erro ao excluir funcionário.");
            console.error("Erro ao excluir funcionário", error);
        }
    };

    return (
        <div>
            <h1>Lista de Funcionários</h1>
            <button onClick={handleLogout} style={{ marginBottom: "10px", cursor: "pointer" }}>
                Logout
            </button>

            {message && <p>{message}</p>}

            <form onSubmit={handleSubmit} style={{ marginBottom: "20px" }}>
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
                <button type="submit">{editandoId ? "Atualizar" : "Adicionar"}</button>
            </form>

            <ul>
                {funcionarios.map((funcionario: any) => (
                    <li key={funcionario.id}>
                        {funcionario.nome} - {funcionario.email}
                        <button onClick={() => handleEdit(funcionario)}>Editar</button>
                        <button onClick={() => handleDelete(funcionario.id)}>Excluir</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default Dashboard;