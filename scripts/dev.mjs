import { existsSync } from "node:fs";
import { spawn, spawnSync } from "node:child_process";
import process from "node:process";

const isWindows = process.platform === "win32";
const localSettings = "ThonInventory/appsettings.Local.json";

if (!existsSync(localSettings)) {
  console.error("Mangler ThonInventory/appsettings.Local.json.");
  console.error("Kopier appsettings.Local.example.json til appsettings.Local.json og legg inn databasepassordet.");
  process.exit(1);
}

if (!existsSync("frontend/node_modules")) {
  console.log("Installerer frontend-pakker...");

  const install = isWindows
    ? spawnSync("cmd.exe", ["/d", "/s", "/c", "npm.cmd --prefix frontend install"], { stdio: "inherit" })
    : spawnSync("npm", ["--prefix", "frontend", "install"], { stdio: "inherit" });

  if (install.status !== 0) {
    process.exit(install.status ?? 1);
  }
}

const frontend = isWindows
  ? spawn("cmd.exe", ["/d", "/s", "/c", "npm.cmd --prefix frontend run dev"], { stdio: "inherit" })
  : spawn("npm", ["--prefix", "frontend", "run", "dev"], { stdio: "inherit" });

const backend = spawn(
  "dotnet",
  ["watch", "--project", "ThonInventory/ThonInventory.csproj", "run", "--launch-profile", "http"],
  { stdio: "inherit" }
);

const children = [frontend, backend];
let stopping = false;

function stop(exitCode = 0) {
  if (stopping) return;
  stopping = true;

  for (const child of children) {
    if (!child.killed) {
      child.kill();
    }
  }

  setTimeout(() => process.exit(exitCode), 150);
}

for (const child of children) {
  child.on("error", (error) => {
    console.error(error.message);
    stop(1);
  });

  child.on("exit", (code) => {
    if (!stopping && code !== null && code !== 0) {
      stop(code);
    }
  });
}

process.on("SIGINT", () => stop(0));
process.on("SIGTERM", () => stop(0));
