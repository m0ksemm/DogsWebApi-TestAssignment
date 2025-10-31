# DogsWebApi-TestAssignment

<h1 align="center"> Dogshouse Service API</h1>

<p>This project is a sample REST API built with <b>ASP.NET Core Web API</b> and <b>Entity Framework Core (Code First)</b>. 
The API provides basic CRUD functionality for managing dogs, as well as support for sorting, pagination, and rate limiting.</p>

<hr/>

<h2> Database</h2>

<p>The database contains a single table named <code>dogs</code>:</p>

<table>
<tr><th>name</th><th>color</th><th>tail_length</th><th>weight</th></tr>
<tr><td>Neo</td><td>red & amber</td><td>22</td><td>32</td></tr>
<tr><td>Jessy</td><td>black & white</td><td>7</td><td>14</td></tr>
</table>

<hr/>

<h2>⚙️ API Endpoints</h2>

<h3>1. Ping</h3>

<pre><code>GET /ping
</code></pre>

<p><b>Response:</b></p>

<pre><code>Dogshouseservice.Version1.0.1
</code></pre>

<hr/>

<h3>2. Get Dogs</h3>

<pre><code>GET /dogs
</code></pre>

<p><b>Response Example:</b></p>

<pre><code>[
  {
    "name": "Neo",
    "color": "red&amber",
    "tail_length": 22,
    "weight": 32
  },
  {
    "name": "Jessy",
    "color": "black&white",
    "tail_length": 7,
    "weight": 14
  }
]
</code></pre>

<h4>Query Parameters</h4>

<table>
<tr><th>Parameter</th><th>Type</th><th>Description</th></tr>
<tr><td><code>attribute</code></td><td>string</td><td>Attribute to sort by (e.g., weight, tail_length, name, color)</td></tr>
<tr><td><code>order</code></td><td>string</td><td>Sorting order: asc (default) or desc</td></tr>
<tr><td><code>pageNumber</code></td><td>int</td><td>Page number (starting from 1)</td></tr>
<tr><td><code>pageSize</code></td><td>int</td><td>Number of records per page</td></tr>
</table>

<p><b>Examples:</b></p>

<pre><code># Sort by weight descending
GET /dogs?attribute=weight&order=desc

# Get page 3 with 10 results per page
GET /dogs?pageNumber=3&pageSize=10

# Combine pagination and sorting
GET /dogs?attribute=tail_length&order=asc&pageNumber=2&pageSize=5
</code></pre>

<hr/>

<h3>3. Create Dog</h3>

<pre><code>POST /dog
</code></pre>

<p><b>Request Body Example:</b></p>

<pre><code>{
  "name": "Doggy",
  "color": "red",
  "tail_length": 173,
  "weight": 33
}
</code></pre>

<p><b>Responses:</b></p>

<ul>
<li> 201 Created – Dog successfully created</li>
<li> 400 Bad Request – Invalid data (e.g., negative tail length, invalid JSON)</li>
<li> 409 Conflict – Dog with the same name already exists</li>
</ul>

<hr/>

<h2> Error Handling</h2>

<p>The API validates all incoming data and handles the following scenarios:</p>

<ul>
<li>Dog with the same name already exists in DB</li>
<li>Tail height is a negative number or is not a number</li>
<li>Invalid JSON is passed in a request body</li>
<li>Other cases that need to be handled in order for API to work properly</li>
</ul>

<hr/>

<h2>🚦 Rate Limiting</h2>

<p>There should be a setting that says how many requests the service can handle, for example, <b>10 requests per second</b>.
In case there are more incoming requests than in configuration, application should return:</p>

<pre><code>HTTP 429 Too Many Requests
</code></pre>

<hr/>

<h2> Technical Details</h2>

<ul>
<li>Framework: ASP.NET Core Web API</li>
<li>Database: MS SQL Server (via EF Core Code First)</li>
<li>ORM: Entity Framework Core</li>
<li>Language: C#</li>
<li>Architecture: Clean architecture, async/await support</li>
<li>Testing: xUnit, FluentAssertions, WebApplicationFactory</li>
</ul>

<hr/>

<h2>🧪 Unit Tests</h2>

<p>All logic in the application should be covered by unit tests, including:</p>

<ul>
<li>Controllers</li>
<li>Services</li>
<li>Validation rules</li>
<li>Rate limiting logic</li>
</ul>

<hr/>

<h2>📄 Version</h2>

<p><b>Dogshouseservice.Version1.0.1</b></p>
