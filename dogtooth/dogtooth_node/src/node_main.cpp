#include <chrono>
#include <memory>
#include <string>

#include <rcutils/cmdline_parser.h>
#include <rclcpp/rclcpp.hpp>
#include "dogtooth_node/diff_drive_controller.hpp"
#include "dogtooth_node/dogtooth.hpp"

void help_print()
{
  printf("For dogtooth node : \n");
  printf("dogtooth_node [-i port] [-h]\n");
  printf("options:\n");
  printf("-h : Print this help function.\n");
  printf("-i port: Connected port with ");
}

int main(int argc, char * argv[])
{
  setvbuf(stdout, NULL, _IONBF, BUFSIZ);

  if (rcutils_cli_option_exist(argv, argv + argc, "-h")) {
    help_print();
    return 0;
  }

  rclcpp::init(argc, argv);
  std::string port = "";
  char * cli_options;
  cli_options = rcutils_cli_get_option(argv, argv + argc, "-i");
  if (nullptr != cli_options) {
    port = std::string(cli_options);
  }

  rclcpp::executors::SingleThreadedExecutor executor;
  auto dogtooth = std::make_shared<robotis::dogtooth::Dogtooth>(port);
  auto diff_drive_controller =
    std::make_shared<robotis::dogtooth::DiffDriveController>(
    dogtooth->get_wheels()->separation,
    dogtooth->get_wheels()->radius);

  executor.add_node(dogtooth);
  executor.add_node(diff_drive_controller);
  executor.spin();

  rclcpp::shutdown();

  return 0;
}
