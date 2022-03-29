CREATE DATABASE escola;

\connect escola

CREATE TABLE public.pessoas (
    id integer NOT NULL,
    nome character varying NOT NULL,
    sobrenome character varying NOT NULL,
    data_nascimento date
);

ALTER TABLE public.pessoas OWNER TO postgres;

--
-- Name: alunos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.alunos (
    matricula integer
)
INHERITS (public.pessoas);


ALTER TABLE public.alunos OWNER TO postgres;

--
-- Name: faxineiros; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.faxineiros (
    salario real
)
INHERITS (public.pessoas);


ALTER TABLE public.faxineiros OWNER TO postgres;

--
-- Name: pessoas_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.pessoas_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.pessoas_id_seq OWNER TO postgres;

--
-- Name: pessoas_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.pessoas_id_seq OWNED BY public.pessoas.id;


--
-- Name: professores; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.professores (
    salario real,
    disciplina character varying
)
INHERITS (public.pessoas);


ALTER TABLE public.professores OWNER TO postgres;

--
-- Name: alunos id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.alunos ALTER COLUMN id SET DEFAULT nextval('public.pessoas_id_seq'::regclass);


--
-- Name: faxineiros id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.faxineiros ALTER COLUMN id SET DEFAULT nextval('public.pessoas_id_seq'::regclass);


--
-- Name: pessoas id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pessoas ALTER COLUMN id SET DEFAULT nextval('public.pessoas_id_seq'::regclass);


--
-- Name: professores id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.professores ALTER COLUMN id SET DEFAULT nextval('public.pessoas_id_seq'::regclass);


--
-- Data for Name: alunos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.alunos (id, nome, sobrenome, data_nascimento, matricula) FROM stdin;
\.


--
-- Data for Name: faxineiros; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.faxineiros (id, nome, sobrenome, data_nascimento, salario) FROM stdin;
\.


--
-- Data for Name: pessoas; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.pessoas (id, nome, sobrenome, data_nascimento) FROM stdin;
\.


--
-- Data for Name: professores; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.professores (id, nome, sobrenome, data_nascimento, salario, disciplina) FROM stdin;
\.


--
-- Name: pessoas_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.pessoas_id_seq', 1, false);


--
-- Name: pessoas pessoas_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.pessoas
    ADD CONSTRAINT pessoas_pkey PRIMARY KEY (id);