#ifndef DOGTOOTH_NODE__DEVICES__RESET_HPP_
#define DOGTOOTH_NODE__DEVICES__RESET_HPP_

#include <memory>
#include <string>

#include <std_srvs/srv/trigger.hpp>
#include "dogtooth_node/devices/devices.hpp"

namespace robotis
{
  namespace dogtooth
  {
    namespace devices
    {
      class Reset : public Devices
      {
        public:
          static void request(
            rclcpp::Client<std_srvs::srv::Trigger>::SharedPtr client,
            std_srvs::srv::Trigger::Request req);

          explicit Reset(
            std::shared_ptr<rclcpp::Node> & nh,
            const std::string & server_name = "reset");

          void command(const void * request, void * response) override;

        private:
          rclcpp::Service<std_srvs::srv::Trigger>::SharedPtr srv_;
      };
    }  // namespace devices
  }  // namespace dogtooth
}  // namespace robotis
#endif  // DOGTOOTH_NODE__DEVICES__RESET_HPP_
