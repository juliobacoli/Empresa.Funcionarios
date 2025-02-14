import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

function Dashboard() {
    const [funcionarios, setFuncionarios] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
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

        fetchFuncionarios();
    }, [navigate]);

    const handleLogout = () => {
        localStorage.removeItem("token");
        navigate("/login");
    };

    return (
        <div>
            <h1>Lista de Funcionários</h1>
            <button onClick={handleLogout} style={{ marginBottom: "10px", cursor: "pointer" }}>
                Logout
            </button>
            <ul>
                {funcionarios.map((funcionario: any) => (
                    <li key={funcionario.id}>
                        {funcionario.nome} - {funcionario.email}
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default Dashboard;
